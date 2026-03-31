using System.Collections.Generic;
using Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public static PlayerController Instance { get; private set; }
        // Input
        private PlayerInput _playerInput;

        enum PlayerState
        {
            Idle,
            Walking,
            Running,
            Crouching,
            Jumping,
        }

        // Player
        public float InputSmoothingSpeed = 10f;
        public float Speed = 0f;
        public PlayerData playerData;
        private CharacterController _controller;
        private readonly float _terminalVelocity = 53.0f;
        private readonly float _fallTimeout = 0.15f;
        private readonly float _jumpTimeout = 0.1f;
        private float _verticalVelocity;
        private Vector3 _velocity;
        private float _fallTimeoutDelta;
        private bool _isGrounded;
        private float _jumpTimeoutDelta;
        private Vector2 _smoothedInput;
        [SerializeField] private float _jumpMagnitude = 2.5f;

        PlayerState _playerState;

        // Game Controllers
        private UserInterfaceController _userInterface;


        // Camera
        public GameObject cinemachineCameraTarget;
        private readonly float _bottomClamp = -90.0f;
        private readonly float _topClamp = 90.0f;
        private readonly float _rotationSpeed = 1.0f;
        private float _rotationVelocity;
        private float _cinemachineTargetPitch;

        private void Awake()
        {
            Instance = this;

            playerData = SaveSystem.LoadPlayer();
        }

        private void OnApplicationQuit()
        {
            playerData.position.x = transform.position.x;
            playerData.position.y = transform.position.y;
            playerData.position.z = transform.position.z;
            playerData.inventoryItems = GetComponent<InventoryController>().GetItemList();
            playerData.equippedItems = GetComponent<EquipmentController>().equippedItems;

            SaveSystem.SavePlayer(playerData);
        }

        private void Start()
        {
            Teleport();
            _playerState = new PlayerState();
            _playerState = PlayerState.Idle;
            _controller = GetComponent<CharacterController>();
            _userInterface = GetComponent<UserInterfaceController>();
            _playerInput = GetComponent<PlayerInput>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _playerInput.OnCrouch += OnCrouch;
            _playerInput.OnPause += OnPause;
            _playerInput.OnSprint += OnSprint;
            _playerInput.OnJump += OnJump;
            _playerInput.OnInteract += OnInteract;

            // INITIALIZE COMPONENTS
            //SaveSystem.SaveSystem.Instance.LoadGame();
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing)
            {
                return;
            }
            CheckGround();
            HandleMovement();
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing)
            {
                return;
            }
            HandleLook();
        }


        private void HandleMovement()
        {
            Vector2 targetInput = _playerInput.GetMovementVectorNormalized();

            _smoothedInput = Vector2.Lerp(_smoothedInput, targetInput, InputSmoothingSpeed * Time.deltaTime);

            Vector3 moveDir = transform.right * _smoothedInput.x + transform.forward * _smoothedInput.y + transform.up * _verticalVelocity;

            if (moveDir == Vector3.zero)
            {
                _playerState = PlayerState.Idle;
                return;
            }

            Vector3 velocity = moveDir * Speed + new Vector3(0f, _verticalVelocity, 0f);

            _controller.Move(velocity * Time.deltaTime);
        }

        private void HandleLook()
        {
            float deltaTimeMultiplier = 1.0f;
            Vector2 vectorInput = _playerInput.GetLookVector();
            //Debug.Log(vectorInput);

            _cinemachineTargetPitch += vectorInput.y * _rotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = vectorInput.x * _rotationSpeed * deltaTimeMultiplier;

            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

            cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            transform.Rotate(Vector3.up * _rotationVelocity);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void CheckGround()
        {
            if (_controller.isGrounded)
            {
                _fallTimeoutDelta = _fallTimeout;

                if (_verticalVelocity < 0.0f) _verticalVelocity = -2f;

                if (_jumpTimeoutDelta >= 0.0f) _jumpTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                _jumpTimeoutDelta = _jumpTimeout;
                if (_fallTimeoutDelta >= 0.0f) _fallTimeoutDelta -= Time.deltaTime;
            }

            if (_verticalVelocity < _terminalVelocity) _verticalVelocity += -9.81f * Time.deltaTime;
        }

        private void OnCrouch(object sender, System.EventArgs e)
        {
            //TODO - Implement crouch
            //SaveGameplayerData playerData = SaveSystem.SaveSystem.SaveGameplayerData;
        }

        private void OnPause(object sender, System.EventArgs e)
        {
            _userInterface.TogglePauseMenu();
        }

        private void OnSprint(object sender, System.EventArgs e)
        {
            if ((InputActionPhase)sender == InputActionPhase.Performed)
            {
                Speed = PlayerConstants.RunSpeed;
                _playerState = PlayerState.Running;
            }

            if ((InputActionPhase)sender == InputActionPhase.Canceled)
            {
                Speed = PlayerConstants.WalkSpeed;
                _playerState = PlayerState.Walking;
            }

        }

        private void OnJump(object sender, System.EventArgs e)
        {
            if (_controller.isGrounded)
            {
                _verticalVelocity = _jumpMagnitude;
            }
        }

        private void OnInteract(object sender, System.EventArgs e)
        {
            float xRotation = _cinemachineTargetPitch;
            float yRotation = transform.eulerAngles.y;

            Quaternion finalRotation = Quaternion.Euler(xRotation, yRotation, 0);
            Vector3 lookDirection = finalRotation * Vector3.forward;

            Vector3 rayOrigin = cinemachineCameraTarget.transform.position;

            float interactDistance = 5.0f;
            RaycastHit hit;

            int layerMask = ~LayerMask.GetMask("Player");

            if (Physics.Raycast(rayOrigin, lookDirection, out hit, interactDistance, layerMask))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact(gameObject);
                }
            }

        }

        public void TakeDamage(float damage)
        {
            playerData.hp -= damage;
            Debug.Log($"Took {damage} damage");

            if (playerData.hp <= 0)
            {
                Debug.Log($"YOU DIED");
            }
        }



        public void Teleport()
        {
            transform.position = new Vector3(playerData.position.x, playerData.position.y, playerData.position.z);
            Physics.SyncTransforms();
            GetComponent<InventoryController>().SetItemList(playerData.inventoryItems);
        }
    }
}
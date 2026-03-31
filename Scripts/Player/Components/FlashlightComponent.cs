using UnityEngine;

namespace Player.Components
{
    public class FlashlightComponent : MonoBehaviour
    {
        PlayerInput _playerInput;
        [SerializeField] GameObject flashlight;
        bool _isFlashlightOn = false;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            flashlight.SetActive(false);
        }

        private void OnEnable()
        {
            _playerInput.OnUseFlashlight += UseFlashlight;
        }

        private void OnDisable()
        {
            _playerInput.OnUseFlashlight -= UseFlashlight;
        }

        void UseFlashlight(object sender, System.EventArgs e)
        {
            _isFlashlightOn = !_isFlashlightOn;
            flashlight.SetActive(_isFlashlightOn);
        }
    }
}

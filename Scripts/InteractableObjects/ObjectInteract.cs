using System;
using Player;
using UnityEngine;

public class ObjectInteract : MonoBehaviour, IInteractable
{
    private bool _playerInRange = false;
    private PlayerInput _playerInput;
    public LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.PlayerPrefab)
        {
            _playerInput = other.GetComponent<PlayerInput>();
            _playerInput.OnInteract += OnInteract;
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _playerInput != null)
        {
            _playerInRange = false;
            _playerInput.OnInteract -= OnInteract;
            _playerInput = null;
        }
    }

    public void OnInteract(object sender, System.EventArgs e)
    {
        Interact(gameObject);
    }

    public void Interact(GameObject interactor)
    { 
        if (_playerInRange)
        {

            Camera camera = Camera.main;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit, 3000f, layerMask ,QueryTriggerInteraction.Ignore))
            {
                Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow); 
                Debug.Log(hit.collider.name);
            }
          
        }
    }

    public void Interact(PlayerController player)
    {
        throw new NotImplementedException();
    }
}
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float InteractionRange = 3f;
    public Transform CameraTransform;
    public LayerMask InteractableLayer;

    private IInteractable _currentInteractable;

    void Update()
    {
        CheckInteractableObject();
    }

    public void CheckInteractableObject()
    {
        Ray ray = new Ray(CameraTransform.position, CameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionRange, InteractableLayer))
        {
            hit.collider.TryGetComponent(out _currentInteractable);
        }
        else
        {
            _currentInteractable = null;
        }
    }

    public void OnInteractTriggered()
    {
        if (_currentInteractable != null)
        {
            //_currentInteractable.Interact();
        }
    }
}

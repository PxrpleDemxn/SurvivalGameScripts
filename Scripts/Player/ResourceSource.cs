using Player;
using UnityEngine;

public abstract class ResourceSource : MonoBehaviour, IInteractable
{
    public string resourceName;
    public int amount = 5;
    
    public void Interact(GameObject interactor)
    {
        Gather(interactor);
    }

    public abstract void Gather(GameObject interactor);
}
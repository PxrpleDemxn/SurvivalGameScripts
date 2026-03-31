using Player.Components;
using UnityEngine;

public class BaseItem : MonoBehaviour, IInteractable
{
    public ItemData ItemData;

    public virtual void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent(out InventoryController inv))
        {
            Collect(inv);
        }
    }

    private void Collect(InventoryController inv)
    {
        bool isAdded = inv.AddItem(ItemData);
        if (isAdded)
        {
            Destroy(gameObject);
        }
    }

}
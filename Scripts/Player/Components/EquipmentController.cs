using System.Collections.Generic;
using Player.Components;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public List<EquippedItem> equippedItems = new List<EquippedItem>();
    private InventoryController inventoryController;

    void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }
    void Start()
    {
        if (inventoryController == null)
        {
            Debug.LogWarning("InventoryController is not assigned. Cannot equip items.");
            return;
        }

        InventoryItem item = inventoryController.GetItem("stonePickaxe");

        if (item != null)
        {
            EquipItem(item);
        }
        else
        {
            Debug.LogWarning("Item 'stonePickaxe' not found in inventory to equip.");
        }
    }

    public InventoryItem equippedItem;

    public void EquipItem(InventoryItem item)
    {
        ItemData itemData = inventoryController.GetItemData(item.id);
        if (itemData != null && itemData.category == ItemCategory.Tool)
        {
            equippedItem = item;
            equippedItems.Add(new EquippedItem(item, "hand"));
            Debug.Log("Equipped item");
        }

    }

    public void UnequipItem(InventoryItem item)
    {
        equippedItem = null;
        equippedItems.RemoveAll(e => e.id == item.id);
        Debug.Log("Unequipped item");
    }
}

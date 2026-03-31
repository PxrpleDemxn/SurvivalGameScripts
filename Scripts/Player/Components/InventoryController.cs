using System;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Components
{
    public class InventoryController : MonoBehaviour
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        public ItemDatabase itemDatabase;

        private int maxItemCount = 16;

        public bool AddItem(ItemData data)
        {
            // 1. Najdi slot se stejným ID, který ještě není plný
            InventoryItem existingSlot = items.Find(i => i.id == data.itemId && i.count < data.maxStackSize);

            if (existingSlot != null)
            {
                // Máme kam přidávat
                existingSlot.count += 1;
                return true;
            }
            else
            {
                if (items.Count >= maxItemCount)
                {
                    Debug.LogWarning("Inventory is full. Cannot add more items.");
                    return false;
                }

                Item newItem = new Item()
                {
                    id = data.itemId,
                    name = data.itemName,
                    description = data.description,
                    maxStackSize = data.maxStackSize,
                    weight = data.weight,
                };

                items.Add(new InventoryItem(newItem));
                return true;
            }
        }

        public InventoryItem GetItem(string itemId)
        {
            InventoryItem foundItem = items.Find(i => i.id == itemId);
            if (foundItem != null)
            {
                return foundItem;
            }
            else
            {
                Debug.LogWarning($"Item not found in inventory: {itemId}");
                return null;
            }
        }

        public ItemData GetItemData(string itemId)
        {
            ItemData data = itemDatabase.GetItemById(itemId);
            if (data != null)
            {
                return data;
            }
            else
            {
                Debug.LogWarning($"ItemData not found in database for itemId: {itemId}");
                return null;
            }
        }

        public List<InventoryItem> GetItemList()
        {
            return items;
        }

        public void SetItemList(List<InventoryItem> itemList)
        {
            items = itemList;
            Debug.Log("Inventory updated. Total items: " + items.Count);
        }
    }
}



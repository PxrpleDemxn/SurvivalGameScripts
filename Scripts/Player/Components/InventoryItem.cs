using System;
using UnityEngine;

namespace Player.Components
{
    [Serializable]
    public class InventoryItem
    {
        public string id;
        public int count;

        public InventoryItem() { }
        public InventoryItem(Item data)
        {
            if (data == null)
            {
                return;
            }

            this.id = data.id;
            this.count += 1;
        }
    }
}
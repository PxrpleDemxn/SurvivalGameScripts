using UnityEngine;

namespace Player.Components
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemId;
        public string itemName;
        public string description;
        public int maxStackSize = 100;
        public int collectAmount = 1;
        public float weight = 1f;
        public ItemType type;
        public ItemCategory category;

    }

    public enum ItemCategory
    {
        Resource,
        Food,
        Tool,
        Misc
    }

    public enum ItemType
    {
        Ore,
        Wood
    }
}
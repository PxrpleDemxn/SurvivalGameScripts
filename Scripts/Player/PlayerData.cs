using System;
using System.Collections.Generic;
using Player.Components;

namespace Player
{
    [Serializable]
    public class PlayerData
    {
        public string playerName = "Player";
        public Position position = new Position();

        public float hp = 100;
        public float maxHp = 100;

        public float stamina = 100;
        public float maxStamina = 100;

        public float hunger = 100;
        public float maxHunger = 100;

        public float thirst = 100;
        public float maxThirst = 100;

        public int level = 1;
        public List<EquippedItem> equippedItems = new List<EquippedItem>();
        public List<InventoryItem> inventoryItems = new List<InventoryItem>();
        public ProfessionData mining = new ProfessionData();
        public ProfessionData herbalism = new ProfessionData();
        public ProfessionData woodcutting = new ProfessionData();
        public ProfessionData fishing = new ProfessionData();
    }

    [Serializable]
    public class ProfessionData
    {
        public int level = 1;
        public float xp = 0;
    }

    [Serializable]
    public class Position
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;
    }
}

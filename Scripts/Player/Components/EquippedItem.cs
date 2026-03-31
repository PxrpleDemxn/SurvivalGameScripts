using System;
using Player.Components;

[Serializable]
public class EquippedItem
{
    public string id;
    public string slot;

    public EquippedItem() { }
    public EquippedItem(InventoryItem data, string slot)
    {
        if (data == null)
        {
            return;
        }

        this.id = data.id;
        this.slot = slot;
    }

}

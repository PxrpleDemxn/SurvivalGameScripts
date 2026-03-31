using UnityEngine;

public class Item
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public Sprite icon { get; set; }
    public float weight { get; set; }
    public int amount { get; set; }
    public int maxStackSize { get; set; }
}

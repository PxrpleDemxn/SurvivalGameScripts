using System.Collections.Generic;
using Player.Components;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> allItems = new List<ItemData>();

    public ItemData GetItemById(string id)
    {
        return allItems.Find(item => item.itemId == id);
    }

    [ContextMenu("Refresh Item Database")]
    public void RefreshDatabase()
    {
#if UNITY_EDITOR
        allItems.Clear();
        // Zkontroluj, zda se tvoje ScriptableObjecty jmenují přesně ItemData
        string[] guids = AssetDatabase.FindAssets("t:ItemData");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            if (item != null)
            {
                allItems.Add(item);
            }
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets(); // Důležité pro trvalé uložení změn v souboru
        Debug.Log($"Database refreshed. Found {allItems.Count} items.");
#endif
    }

}
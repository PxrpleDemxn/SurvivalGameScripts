using System;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "ScriptableObject/QuestInfo", order = 1)]
public class QuestInfo : ScriptableObject
{
    [field:  SerializeField] public string id { get; private set; }

    public string questName;
    public QuestInfo[] questRequirements;

    public GameObject[] questPrefabs;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}

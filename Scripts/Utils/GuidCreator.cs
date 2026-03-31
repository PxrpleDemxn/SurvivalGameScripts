using UnityEngine;
using static System.Guid;

[ExecuteAlways]
public class GuidCreator : MonoBehaviour
{
    [SerializeField] private string uniqueId = NewGuid().ToString();

    public string UniqueId => uniqueId;

    public string GetGuid()
    {
        return uniqueId;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            uniqueId = NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}

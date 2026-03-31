using UnityEngine;

public class PlayerUtils : MonoBehaviour
{
    public RaycastHit GetRaycastHit(Camera camera, LayerMask layerMask, float distance)
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit, distance, layerMask))
        {
        }
        
        return hit;
    }
}

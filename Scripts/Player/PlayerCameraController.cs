using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;

    public Transform orientation;
    public Transform cameraRoot;

    private float xRotation;
    private float yRotation;

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera root (pitch only)
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate orientation (yaw)
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
using UnityEngine;

public class PlayerCursor
{

    public void SetGameMouseMode()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void SetMenuMouseMode()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public event EventHandler OnCrouch;
    public event EventHandler OnJump;
    public event EventHandler OnReload;
    public event EventHandler<int> OnWeaponSwitch;
    public event EventHandler OnUseFlashlight;
    public event EventHandler OnPause;
    public event EventHandler OnSprint;
    public event EventHandler OnInteract;
    public event EventHandler OnInventory;

    public PlayerInteraction PlayerInteraction;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Player.Crouch.performed += Crouch;
        _playerInputActions.Player.Jump.performed += Jump;
        _playerInputActions.Player.Reload.performed += Reload;
        _playerInputActions.Player.SwitchWeapon.performed += SwitchWeaponPerformed;
        _playerInputActions.Player.UseFlashlight.performed += UseFlashlight;
        _playerInputActions.Player.Pause.performed += Pause;
        _playerInputActions.Player.Sprint.performed += Sprint;
        _playerInputActions.Player.Sprint.canceled += Sprint;
        _playerInputActions.Player.Interact.performed += Interact;
        _playerInputActions.Player.Inventory.performed += Inventory;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Crouch.performed -= Crouch;
        _playerInputActions.Player.Jump.performed -= Jump;
        _playerInputActions.Player.Reload.performed -= Reload;
        _playerInputActions.Player.SwitchWeapon.performed -= SwitchWeaponPerformed;
        _playerInputActions.Player.UseFlashlight.performed -= UseFlashlight;
        _playerInputActions.Player.Pause.performed -= Pause;
        _playerInputActions.Player.Sprint.performed -= Sprint;
        _playerInputActions.Player.Inventory.performed -= Inventory;
        _playerInputActions.Player.Interact.performed -= Interact;


        _playerInputActions.Dispose();
    }


    public Vector2 GetMovementVectorNormalized()
    {
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public Vector2 GetLookVector()
    {
        var inputVector = _playerInputActions.Player.Look.ReadValue<Vector2>();
        return inputVector;
    }

    public void Crouch(InputAction.CallbackContext ctx)
    {
        OnCrouch?.Invoke(this, EventArgs.Empty);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    public void Reload(InputAction.CallbackContext ctx)
    {
        OnReload?.Invoke(this, EventArgs.Empty);
    }

    public void UseFlashlight(InputAction.CallbackContext ctx)
    {
        OnUseFlashlight?.Invoke(this, EventArgs.Empty);
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {

        OnSprint?.Invoke(ctx.phase, EventArgs.Empty);
    }

    public void Inventory(InputAction.CallbackContext ctx)
    {
        OnInventory?.Invoke(ctx.phase, EventArgs.Empty);
    }

    public void SwitchWeaponPerformed(InputAction.CallbackContext ctx)
    {
        float scrollDelta = ctx.ReadValue<float>();

        int direction = 0;
        if (scrollDelta > 0.1f)
        {
            direction = 1; // up
        }
        else if (scrollDelta < -0.1f)
        {
            direction = -1; // down
        }

        if (direction != 0)
        {
            OnWeaponSwitch?.Invoke(this, direction);
        }

        // Alternative for number keys (if you mapped them this way)
        // int weaponIndex = -1;
        // if (ctx.action == _playerInputActions.Player.Weapon1) weaponIndex = 0;
        // else if (ctx.action == _playerInputActions.Player.Weapon2) weaponIndex = 1;
        // // ... etc.
        // if (weaponIndex != -1) OnWeaponSwitch?.Invoke(this, weaponIndex);
    }

    public float GetFire()
    {
        var input = _playerInputActions.Player.Fire.ReadValue<float>();
        return input;
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }



}
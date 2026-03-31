using UnityEngine;

public class WeaponSystemController : MonoBehaviour
{
    public Weapon[] weapons;

    private Weapon _currentWeapon;
    
    [SerializeField] private PlayerInput _playerInput;
    public GameObject weaponHolder;

    private int _currentWeaponIndex = 0;

    private void Awake()
    {
        if (_playerInput == null) _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        if (weapons == null || weapons.Length == 0)
        {
            Debug.LogWarning("There are no weapons in the scene.");
        }
        
        if (weapons.Length > 0)
        {
            foreach (Weapon weapon in weapons)
            {
                Instantiate(weapon.prefab, weaponHolder.transform);
            }
            _currentWeapon = weapons[_currentWeaponIndex];
            ShowCurrentWeapon();
            Debug.Log($"Current weapon: {_currentWeapon.weaponName}");
        }
        else
        {
            Debug.LogError("No weapons defined in the WeaponSystemController!");
        }
        
        if (_playerInput != null)
        {
            _playerInput.OnReload += Reload;
            _playerInput.OnWeaponSwitch += SwitchWeapon;
        }
        else
        {
            Debug.LogError("PlayerInput reference is missing in WeaponSystemController!");
        }
    }

    void Update()
    {
        if (_currentWeapon == null) return;

        if (Mathf.Approximately(_playerInput.GetFire(), 1))
        {
            _currentWeapon.Fire();
        }
    }

    private void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.OnReload -= Reload;
            _playerInput.OnWeaponSwitch -= SwitchWeapon;
        }
    }

    private void Reload(object sender, System.EventArgs e)
    {
        Debug.Log("Reload");
        if (_currentWeapon != null)
        {
            _currentWeapon.Reload();
        }
    }

    private void SwitchWeapon(object sender, int direction)
    {
        Debug.Log(direction);
        if (weapons == null || weapons.Length == 0) return;

        _currentWeaponIndex += direction;
        
        if (_currentWeaponIndex >= weapons.Length)
        {
            _currentWeaponIndex = 0;
        }
        else if (_currentWeaponIndex < 0)
        {
            _currentWeaponIndex = weapons.Length - 1;
        }

        _currentWeapon = weapons[_currentWeaponIndex];
        ShowCurrentWeapon();
        Debug.Log($"Switched to: {_currentWeapon.weaponName}");
    }

    private void ShowCurrentWeapon()
    {
        for (int i = 0; i < weaponHolder.transform.childCount; i++)
        {
            Transform child = weaponHolder.transform.GetChild(i);
            child.gameObject.SetActive(i == _currentWeaponIndex);
        }
    }
}
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    [field: SerializeField] public Image StaminaBarFill { get; private set; }
    [field: SerializeField] public GameObject PauseMenuGameObject { get; private set; }
    [field: SerializeField] public GameObject OptionsPanelGameObject { get; private set; }
    [field: SerializeField] public GameObject PauseMenuButtonsPanelGameObject { get; private set; }
    private bool _isPauseMenuVisible = false;
    private PlayerCursor _playerCursor;
    public static UserInterfaceController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _playerCursor = new PlayerCursor();

        //PlayerAttributes.OnStaminaChanged += SetStaminaBarFillAmount;
    }

    private void OnDestroy()
    {
        //PlayerAttributes.OnStaminaChanged -= SetStaminaBarFillAmount;
    }

    public void HideAllUI()
    {
        if (StaminaBarFill != null) StaminaBarFill.enabled = false;
    }

    public void ShowAllUI()
    {
        if (StaminaBarFill != null) StaminaBarFill.enabled = true;
    }

    public void HidePlayerStatsUI() { HideAllUI(); }
    public void ShowPlayerStatsUI() { ShowAllUI(); }

    public void SetStaminaBarFillAmount(float amount, float maxAmount)
    {
        if (StaminaBarFill != null)
        {
            StaminaBarFill.fillAmount = amount / maxAmount;
        }
        else
        {
            Debug.LogError("StaminaBarFill is null when trying to set fill amount!");
        }
    }

    public void TogglePauseMenu()
    {
        _isPauseMenuVisible = !_isPauseMenuVisible;

        PauseMenuGameObject.SetActive(_isPauseMenuVisible);

        if (_isPauseMenuVisible)
        {
            _playerCursor.SetMenuMouseMode();
            GameManager.Instance.SetGameState(GameManager.GameState.Paused);
        }

        else
        {
            _playerCursor.SetGameMouseMode();
            OptionsBack();
            GameManager.Instance.SetGameState(GameManager.GameState.Playing);
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Maps/TC_MainMenu");
        GameManager.Instance.SetGameState(GameManager.GameState.MainMenu);
    }

    public void Continue()
    {
        TogglePauseMenu();
    }

    public void Options()
    {
        // TODO - Create options ui
        PauseMenuButtonsPanelGameObject.SetActive(false);
        OptionsPanelGameObject.SetActive(true);
    }


    public void OptionsBack()
    {
        OptionsPanelGameObject.SetActive(false);
        PauseMenuButtonsPanelGameObject.SetActive(true);
    }

    public void OptionsApply()
    {
        // TODO - APPLY SETTINGS
        OptionsBack();
    }

    public void TestButton()
    {
        Debug.Log("Button pressed");
    }
}

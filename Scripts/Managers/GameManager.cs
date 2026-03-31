using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<int> OnLevelChanged;
    private int _level = 1;
    private PlayerCursor _playerCursor;

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public GameState CurrentGameState { get; set; }

public static event Action<GameState> OnGameStateChanged;
    public static event Action OnPlayerRespawned;

    [Header("Player Management")] 
    public GameObject PlayerPrefab;
    public Transform PlayerSpawnPoint;
    public GameObject CurrentPlayerInstance;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        SetGameState(GameState.MainMenu);
        
        _playerCursor = new  PlayerCursor();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            PlayerSpawnPoint = GameObject.FindWithTag("PlayerSpawn")?.transform;
            
            if (PlayerSpawnPoint != null)
            {
                SpawnPlayer();
            }
            else
            {
                Debug.LogError("No PlayerSpawn point found in the gameplay scene!");
            }
        }
    }

    public void SetGameState(GameState newGameState)
    {
        if (CurrentGameState == newGameState) return;

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(CurrentGameState);

        switch (CurrentGameState)
        {
            case GameState.MainMenu:
                _playerCursor.SetMenuMouseMode();
                Time.timeScale = 1f;
                break;
            case GameState.Playing:
                _playerCursor.SetGameMouseMode();
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                _playerCursor.SetMenuMouseMode();
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                _playerCursor.SetMenuMouseMode();
                Time.timeScale = 0f;
                break;
        }
    }
    
    public void SpawnPlayer()
    {
        if (PlayerPrefab == null || PlayerSpawnPoint == null)
        {
            Debug.LogError("Player Prefab or Spawn Point not assigned in GameManager!");
            return;
        }

        if (CurrentPlayerInstance != null)
        {
            Destroy(CurrentPlayerInstance);
        }

        CurrentPlayerInstance = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
        OnPlayerRespawned?.Invoke();
        SetGameState(GameState.Playing);
    }

    public void SetLevel(int level)
    {
        _level = level;
        OnLevelChanged?.Invoke(_level);
    }

    public void AddLevel()
    {
        _level++;
        OnLevelChanged?.Invoke(_level);
    }

    public int GetLevel()
    {
        return _level;
    }
}

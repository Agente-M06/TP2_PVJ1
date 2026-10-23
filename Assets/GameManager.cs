using UnityEngine;

public enum GameState
{
    StartMenu,
    Playing,
    Paused,
    Victory,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Estado")]
    public GameState currentState = GameState.StartMenu;

    [Header("Objetivos")]
    public int currentScore = 0;
    public int requiredScoreToWin = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetState(GameState.Playing);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                SetState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                SetState(GameState.Playing);
            }
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("[GameManager] Estado: JUGANDO | Tiempo reanudado.");
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("[GameManager] Estado: PAUSA | Juego congelado.");
                break;

            case GameState.Victory:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("[GameManager] Estado: VICTORIA | ¡Misión cumplida! Datos asegurados.");
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("[GameManager] Estado: DERROTA | El agente ha caído.");
                break;
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log($"[Puntuación] +{amount} pts | Total: {currentScore}/{requiredScoreToWin}");
    }

    public bool CanExtract()
    {
        return currentScore >= requiredScoreToWin;
    }
}
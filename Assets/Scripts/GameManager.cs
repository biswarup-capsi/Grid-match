using TMPro;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("UI Elements")]
    public GameObject startPanel;
    public GameObject nextPanel;
    public GameObject pausePanel;
    public GameObject overPanel;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI levelText;

    public TextMeshProUGUI LevelText => levelText;

    public GameState currentState = GameState.NotRunning;

    private int score = 0;
    private int scoreThreshold = 10;
    private int level = 1;
    public int Level => level;


    public bool IsRunning => currentState == GameState.Running;

    private void Awake()
    {
        Screen.fullScreen=false;
        Instance = this;

    }

    private void Start()
    {
        ShowStartPanel();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Running)
                PauseGame();
            else if (currentState == GameState.Paused)
                ResumeGame();
        }
    }

    public void ResetGame()
    {
        GridSpawner.Instance.ResetGrid();
        currentState = GameState.Running;
        Time.timeScale = 1f;
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
        UpdateUI();
    }


    public void PauseGame()
    {

        currentState = GameState.Paused;
        Time.timeScale = 0f;
        UpdateUI();
    }


    public void ResumeGame()
    {
        if (currentState == GameState.Over)
        {
            ResetGame();
            return;
        }

        currentState = GameState.Running;
        Time.timeScale = 1f;
        UpdateUI();
    }



    public void GameOver()
    {
        Handheld.Vibrate();
        currentState = GameState.Over;
        Time.timeScale = 0f;
        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLevel()
    {
        HapticFeedback.HeavyFeedback();
        if (score >= scoreThreshold)
        {
            scoreThreshold += 10;
            //levelText.text = "Level: " + (++level);
        }
    }

    private void UpdateUI()
    {
        if (currentState == GameState.Paused)
            ShowPausePanel();
        else if (currentState == GameState.Over)
            ShowOverPanel();
        else if (currentState == GameState.NotRunning)
            ShowStartPanel();
        else if (currentState == GameState.Running)
            DisableAllPanels();
    }

    public void DisableAllPanels()
    {
        startPanel.SetActive(false);
        nextPanel.SetActive(false);
        overPanel.SetActive(false);
        pausePanel.SetActive(false);

        Debug.Log("All Panels Disabled");
    }

    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
        nextPanel.SetActive(false);
        overPanel.SetActive(false);
        nextPanel.SetActive(false);

    }

    public void ShowPausePanel()
    {
        startPanel.SetActive(false);
        nextPanel.SetActive(false);
        pausePanel.SetActive(true);
        overPanel.SetActive(false);
    }

    public void ShowLevelPanel()
    {
        startPanel.SetActive(false);
        nextPanel.SetActive(true);
        pausePanel.SetActive(false);
        overPanel.SetActive(false);
    }

    public void ShowOverPanel()
    {
        startPanel.SetActive(false);
        nextPanel.SetActive(false);
        overPanel.SetActive(true);
        nextPanel.SetActive(false);

    }

    public void Quit()
    {
        Application.Quit();
    }

}

public enum GameState
{
    NotRunning,
    NextLevel,
    Running,
    Paused,
    Over
}

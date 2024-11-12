using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenuUI;    // Pause menu UI panel
    public Button startButton;        // Button to resume the game
    public Button quitButton;         // Button to quit the game
    public GameObject gameUI;         // Main game UI (to hide when paused)
    public EventSystem eventSystem;   // Reference to the EventSystem for controller navigation

    private bool isPaused = false;    // Track if the game is paused

    void Start()
    {
        // Initialize pause menu (hidden initially)
        pauseMenuUI.SetActive(false);

        // Set up button listeners
        startButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);

        // Set the first selected button to 'Start' for controller navigation
        eventSystem.SetSelectedGameObject(startButton.gameObject);
    }

    void Update()
    {
        // Toggle pause on Escape or controller "Submit" (A button)
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // Pause the game and show the pause menu
    void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);   // Show pause menu
        gameUI.SetActive(false);       // Hide main game UI
        Time.timeScale = 0f;           // Pause game (stop time)
    }

    // Resume the game (hide pause menu and resume time)
    void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);  // Hide pause menu
        gameUI.SetActive(true);        // Show main game UI
        Time.timeScale = 1f;           // Resume game (restore time)
    }

    // Quit the game (in both Editor and build)
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop playmode in Unity Editor
#else
            Application.Quit();  // Quit the game in a built version
#endif
    }
}


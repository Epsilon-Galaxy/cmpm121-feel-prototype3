using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    private EventSystem eventSystem;

    private void Start()
    {
        // Get the EventSystem (handles input for UI elements)
        eventSystem = EventSystem.current;

        // Assign listeners to buttons
        startButton.onClick.AddListener(OnStartButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        // Set the first selected button to be Start (for controller navigation)
        eventSystem.SetSelectedGameObject(startButton.gameObject);
    }

    private void Update()
    {
        // Handle controller input for navigating the menu
        if (Input.GetButtonDown("Submit")) // Usually 'A' or 'Enter' on most controllers
        {
            if (eventSystem.currentSelectedGameObject == startButton.gameObject)
            {
                OnStartButtonClicked();
            }
            else if (eventSystem.currentSelectedGameObject == exitButton.gameObject)
            {
                OnExitButtonClicked();
            }
        }

        // You can also add support for navigating between buttons using the D-Pad or Left Stick
        if (Input.GetButtonDown("Vertical")) // Use up/down navigation with the controller
        {
            if (eventSystem.currentSelectedGameObject == startButton.gameObject)
            {
                eventSystem.SetSelectedGameObject(exitButton.gameObject);
            }
            else if (eventSystem.currentSelectedGameObject == exitButton.gameObject)
            {
                eventSystem.SetSelectedGameObject(startButton.gameObject);
            }
        }
    }

    private void OnStartButtonClicked()
    {
        // Load the main game scene
        SceneManager.LoadScene("GameScene");
    }

    private void OnExitButtonClicked()
    {
        // If testing in Unity Editor, use this to stop the playmode:
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the game in a built application
            Application.Quit();
        #endif

        // OR just quit the game
        //Application.Quit();

    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeToRelaxZone : MonoBehaviour
{
    [SerializeField] private string relaxZoneSceneName;
    [SerializeField] private string trackerSceneName = "TrackerPage";
    [SerializeField] private string creditsSceneName = "Credits";
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject settings;
    private InputAction escapeAction;

    private void Awake()
    {
        escapeAction = new InputAction("Escape", binding: "<Keyboard>/escape");
        escapeAction.performed += ctx => LoadSettings();

        escapeAction.Enable();
    }

    private void OnDestroy()
    {
        escapeAction.Disable();
    }

    private void LoadSettings()
    {
        if (audioManager != null) audioManager.StopSound();
        settings.SetActive(true);
    }

    public void LoadRelaxZone()
    {
        SceneManager.LoadScene(relaxZoneSceneName);
    }

    public void LoadTracker()
    {
        SceneManager.LoadScene(trackerSceneName);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void quitProgram()
    {
        Application.Quit();
    }
}
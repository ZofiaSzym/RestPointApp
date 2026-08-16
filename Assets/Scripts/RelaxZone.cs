using UnityEngine;
using UnityEngine.SceneManagement;

public class RelaxZone : MonoBehaviour
{
    [SerializeField] private string breathingScene;
    [SerializeField] private string musicScene;
    [SerializeField] private string hamsterScene;
    [SerializeField] private string canvasScene;
    [SerializeField] private string ventScene;
    [SerializeField] private string groundingScene;

    public void onBreathingButtonClicked()
    {
        SceneManager.LoadScene(breathingScene);
    }

    public void onMusicButtonClicked()
    {
        SceneManager.LoadScene(musicScene);
    }

    public void onHamsterButtonClicked()
    {
        SceneManager.LoadScene(hamsterScene);
    }

    public void onCanvasButtonClicked()
    {
        SceneManager.LoadScene(canvasScene);
    }

    public void onVentButtonClicked()
    {
        SceneManager.LoadScene(ventScene);
    }

    public void onGroundingButtonClicked()
    {
        SceneManager.LoadScene(groundingScene);
    }
}
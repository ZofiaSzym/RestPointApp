using UnityEngine;
using UnityEngine.SceneManagement;

public class goToRelaxZone : MonoBehaviour
{
    public void onRelaxZoneButtonClicked()
    {
        SceneManager.LoadScene("RelaxZone");
    }
}
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip seaSound;
    [SerializeField] private AudioClip rainSound;
    [SerializeField] private AudioClip music;
    private AudioSource currentSound;

    private void OnApplicationQuit()
    {
        StopSound();
    }

    private void PlaySound(AudioClip clip)
    {
        if (currentSound != null)
        {
            currentSound.Stop();
            Destroy(currentSound);
        }

        currentSound = gameObject.AddComponent<AudioSource>();
        currentSound.clip = clip;
        currentSound.loop = true;
        currentSound.Play();
    }

    public void StopSound()
    {
        if (currentSound != null)
        {
            currentSound.Stop();
            Destroy(currentSound);
            currentSound = null;
        }
    }

    public void OnSeaButtonClicked()
    {
        PlaySound(seaSound);
    }

    public void OnRainButtonClicked()
    {
        PlaySound(rainSound);
    }

    public void OnMusicButtonClicked()
    {
        PlaySound(music);
    }
}
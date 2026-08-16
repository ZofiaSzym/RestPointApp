using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] private VideoClip[] videoClips;
    [SerializeField] private VideoPlayer videoPlayer;
    private int currentVideoIndex = -1;

    private void Start()
    {
        var randomIndex = Random.Range(0, videoClips.Length);
        videoPlayer.clip = videoClips[randomIndex];
    }
}
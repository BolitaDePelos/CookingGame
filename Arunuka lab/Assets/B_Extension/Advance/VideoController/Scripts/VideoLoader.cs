using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] private string nameVideo;
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,nameVideo);
        videoPlayer.Prepare();
        videoPlayer.Play();
    }
}

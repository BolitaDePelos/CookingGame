using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    [SerializeField] SliderCheck sliderChecker;
    [SerializeField] RawImage rawImage;
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] VideoPlayer player;
    [SerializeField] Image progress;
    [SerializeField] TogglePlay togglePlay;

    private void Start()
    {
        togglePlay.Configure(TogglePlayPause);
    }

    private void OnEnable()
    {
        sliderChecker.OnPoint += TrySkip;
    }


    private void OnDisable()
    {
        sliderChecker.OnPoint -= TrySkip;
    }

    void Update()
    {
        if (player.frameCount > 0)
            progress.fillAmount = (float)player.frame / (float)player.frameCount;
    }

    private void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint))
        {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
    }
    private void SkipToPercent(float pct)
    {
        var frame = player.frameCount * pct;
        player.frame = (long)frame;
    }

    private void TogglePlayPause(bool play) 
    {
        if (play)
            VideoPlayerPlay();
        else
            VideoPlayerPause();
    }

    public void VideoPlayerPause()
    {
        if(player != null)
            player.Pause();
    }
    public void VideoPlayerPlay()
    {
        if(player != null)
            player.Play();  
        
    } 
}

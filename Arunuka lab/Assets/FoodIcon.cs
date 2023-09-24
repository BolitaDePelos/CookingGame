using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display an icon based on the emotion.
/// </summary>
public class FoodIcon : MonoBehaviour
{
    [SerializeField] private List<Sprite> emotionIcons;
    [SerializeField] private Image iconImage;
    [SerializeField] private float destroyAfter = 2.0f;

    private float _elapsedSeconds;

    /// <summary>
    /// Called every game frame.
    /// </summary>
    private void Update()
    {
        if (_elapsedSeconds > destroyAfter)
            DestroyImmediate(gameObject);

        _elapsedSeconds += Time.deltaTime;
    }

    /// <summary>
    /// Sets the icon based on the emotion.
    /// </summary>
    public void SetIcon(Emotion emotion)
    {
        iconImage.sprite = emotion switch
        {
            Emotion.Bad => emotionIcons[0],
            Emotion.Normal => emotionIcons[1],
            Emotion.Good => emotionIcons[2],
            _ => iconImage.sprite
        };
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ScreenLoadingDemo : MonoBehaviour, ILoadingScreen
{
    [SerializeField] private GameObject imageLoading;
    [SerializeField] private Image barLoading;

    public void LoadingProgress(float progress)
    {
        barLoading.fillAmount = progress;
    }

    public void OnEndLoading()
    {
        imageLoading.SetActive(false);
    }

    public void OnStartLoading()
    {
        imageLoading.SetActive(true);
    }
}

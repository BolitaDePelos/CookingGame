public interface ILoadingScreen
{
    public void OnStartLoading();
    public void LoadingProgress(float progress);
    public void OnEndLoading();
}
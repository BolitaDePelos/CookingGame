using B_Extensions;
using B_Extensions.SceneLoader;

public class ButtonQuitApplication: BaseButtonAttendant
{
    private void Start() => buttonComponent.onClick.AddListener(()=>SceneLoader.Instance.Quit());
}

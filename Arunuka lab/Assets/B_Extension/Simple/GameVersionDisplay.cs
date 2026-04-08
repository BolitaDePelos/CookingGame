using TMPro;
using UnityEngine;

public class GameVersionDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshVersion;
    void Start() => textMeshVersion.text = Application.version;
}

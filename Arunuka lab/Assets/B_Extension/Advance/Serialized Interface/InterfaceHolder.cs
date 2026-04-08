using UnityEngine;

public class InterfaceHolder : MonoBehaviour
{
    [RequireBadInterface(typeof(DemoInterface))]
    [SerializeField] MonoBehaviour conInterface;
}

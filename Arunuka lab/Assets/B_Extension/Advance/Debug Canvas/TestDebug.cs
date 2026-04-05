using UnityEngine;

public class TestDebug : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("print");
            Debug.Log("this is a log");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogError("this is a error");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogWarning("this is warning");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMainMenu : MonoBehaviour
{

    [Header("UI")]
    private bool AnyKeyPressed = false;

    private static InputMainMenu instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static InputMainMenu GetInstance()
    {
        return instance;
    }

    public void AnyButtonPressed(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            AnyKeyPressed = true;
        }
        else if (context.canceled)
        {
            AnyKeyPressed = false;
        }
    }

    public bool GetAnyPressed()
    {
        bool result = AnyKeyPressed;
        AnyKeyPressed = false;
        return result;
    }


    public bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return GetComponent<PlayerInput>().currentControlScheme == "KeyboardMouse";
#else
            return false;
#endif
        }

    }
}

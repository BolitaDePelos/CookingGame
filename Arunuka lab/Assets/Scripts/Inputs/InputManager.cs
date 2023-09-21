using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    [Header("UI")]
    private bool AnyKeyPressed = false;


    [Header("Character Input")]
    private Vector2 move = Vector2.zero;
    private Vector2 look = Vector2.zero;

    private bool interactPressed = false;
    private bool DropPressed = false;
    private bool LeftButtonMousePressed = false;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    private bool cursorLocked = true;
    private bool cursorInputForLook = true;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //  Event Movement
        if (context.performed || context.canceled)
        {
            GameEventsManager.instance.InputEvents.MovePressed(context.ReadValue<Vector2>());
        }

        if (context.performed)
        {
            move = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            move = context.ReadValue<Vector2>();

        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (cursorInputForLook)
        {
            look = context.ReadValue<Vector2>();

        }
    }


    public void InteractButtonPressed(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            GameEventsManager.instance.InputEvents.InteractionPressed();
            Debug.Log("Button Interact Pressed");

        }

        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void DropButtonPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.InputEvents.DropPressed();
            Debug.Log("Button Drop Pressed");
        }

        if (context.performed)
        {
            DropPressed = true;
        }
        else if (context.canceled)
        {
            DropPressed = false;
        }
    }

    public void LeftMouseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.InputEvents.UsePressed();

            //Debug.Log("Button Use Pressed");

        }

        if (context.performed)
        {
            LeftButtonMousePressed = true;
        }
        else if (context.canceled)
        {
            LeftButtonMousePressed = false;
        }
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



    public Vector2 GetMoveDirection()
    {
        return move;
    }

    public Vector2 GetlookInput()
    {
        return look;
    }


    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetDropPressed()
    {
        bool result = DropPressed;
        DropPressed = false;
        return result;
    }

    public bool GetLeftMousePressed()
    {
        bool result = LeftButtonMousePressed;
        LeftButtonMousePressed = false;
        return result;
    }

    public bool GetAnyPressed()
    {
        bool result = AnyKeyPressed;
        AnyKeyPressed = false;
        return result;
    }


    private void OnApplicationFocus(bool focus)
    {
        SetCursorState(cursorLocked);
    }
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
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

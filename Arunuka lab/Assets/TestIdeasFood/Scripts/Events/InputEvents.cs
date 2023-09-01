using UnityEngine;
using System;

public class InputEvents
{
    public event Action<Vector2> OnMovePressed;
    public void MovePressed(Vector2 moveDir)
    {
        if (OnMovePressed != null)
        {
            OnMovePressed(moveDir);
        }
    }

    public event Action OnInteractionPressed;
    public void InteractionPressed()
    {
        if (OnInteractionPressed != null)
        {
            OnInteractionPressed();
        }
    }

    public event Action OnDropPressed;
    public void DropPressed()
    {
        if (OnDropPressed != null)
        {
            OnDropPressed();
        }
    }


    public event Action OnUsePressed;
    public void UsePressed()
    {
        if (OnUsePressed != null)
        {
            OnUsePressed();
        }
    }


}

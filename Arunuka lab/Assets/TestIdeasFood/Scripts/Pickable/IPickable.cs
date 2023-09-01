using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{

    bool keepWorldPosition { get; }

    GameObject PickUp();

}

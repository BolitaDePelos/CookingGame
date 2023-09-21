using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookinCamera : MonoBehaviour
{
    Camera cam = null;
    Transform _transform;
    void Start()
    {
        cam = Camera.main;
        _transform = transform;
    }
    void Update()
    {
        _transform.rotation = Quaternion.LookRotation(_transform.position, Vector3.forward);
    }
}

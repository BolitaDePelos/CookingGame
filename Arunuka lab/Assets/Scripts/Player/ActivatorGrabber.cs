using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorGrabber : MonoBehaviour
{
    [SerializeField]
    private Grabber grabber;
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.name=="Player") {
            grabber.enabled = true;
        }
    }

}

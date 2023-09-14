using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    [SerializeField]
    private int cuttableLayer=8;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Carrot") {
           // Debug.Log("CUTTABLE");
        }

        if (other.gameObject.layer == cuttableLayer) {
            Debug.Log("CUTTABLE");
            other.gameObject.transform.position = other.GetComponent<Food>().initPos;
//            other.GetComponent<Food>().

        }
      //  if (other.gameObject.tag == "Floor") {        transform.position= initPos; }
    }
}

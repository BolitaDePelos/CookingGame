using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThershold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;


    private void Update()
    {

        bool pourCheck = CalculatePourAngle() < pourThershold;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }



    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
        print("Start");


    }

    private void EndPour()
    {
        print("End");



    }

    private float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;


    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();


    }


}

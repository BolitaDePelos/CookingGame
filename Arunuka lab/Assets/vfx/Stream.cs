using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
   private LineRenderer lineRendererer = null;
    private Vector3 TargetPosition = Vector3.zero;

    private void Awake()
    {
        lineRendererer = GetComponent<LineRenderer>();

    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(BeginPour());

    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            TargetPosition = FindEndPoint();
            MoveToPosition(0,transform.position);
            MoveToPosition (1, TargetPosition);

            yield return null;
        }
        

    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit,2.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f) ;


        return endPoint;
    }
     

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRendererer.SetPosition(index, targetPosition);
    }

}

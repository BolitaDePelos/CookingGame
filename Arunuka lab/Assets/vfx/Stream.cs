using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
   private LineRenderer lineRendererer = null;
    private ParticleSystem splashparticle = null;

    private Coroutine pourRoutine = null;   
    private Vector3 TargetPosition = Vector3.zero;
    private Vector3 targetPosition;

    private void Awake()
    {
        lineRendererer = GetComponent<LineRenderer>();
        splashparticle = GetComponentInChildren<ParticleSystem>();

    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(BeginPour());
        pourRoutine = StartCoroutine(BeginPour());

    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            TargetPosition = FindEndPoint();
            MoveToPosition(0,transform.position);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
        

    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());

    }


    private IEnumerator EndPour()
    {
        while(!HasReachedPosition(0, targetPosition))
        {
            AnimateToPosition(0 , targetPosition);
            AnimateToPosition (1 , targetPosition);
            yield return null;
        }
        Destroy(gameObject);

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


    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRendererer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRendererer.SetPosition (index, newPosition);

    }

    private bool HasReachedPosition(int index, Vector3 targetPosition )
    {
        Vector3 currentPosition = lineRendererer.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
        splashparticle.gameObject.transform.position = transform.position;

        bool isHitting = HasReachedPosition(1, targetPosition);
        splashparticle.gameObject.SetActive(isHitting);
        yield return null;

    }
}

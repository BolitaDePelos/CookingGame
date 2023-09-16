using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroveManager : MonoBehaviour
{
    [SerializeField]
    Transform thrownFoodPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSlicesPosition(List<GameObject> sliceParents) {
        for (int idx = 0; idx < sliceParents.Count; idx++)
        {
            sliceParents[idx].transform.position = thrownFoodPoint.transform.position;

        }


    }
}

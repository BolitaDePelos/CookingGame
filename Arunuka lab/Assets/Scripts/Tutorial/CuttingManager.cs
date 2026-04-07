using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuttingManager : SingletonMonobehaviour<CuttingManager>,IKitchenSector
{
    [SerializeField] private List<Food> ingredients;
    [SerializeField] private int currentItemsCount = 0;
    [SerializeField] private int totalItemsCount;
    [SerializeField] private List<GameObject> sliceParents;
    [SerializeField] private UnityEvent OnCutEnds;
    [SerializeField] private StroveManager stroveManager;
    [SerializeField] private Knife knife;
    [SerializeField] private Transform boardCut;
    public bool tutorialMode;
    public bool onCutDone;

    private void Start() => knife.onDrop.AddListener(MoveBoard);
    public void AddSliceItem(GameObject parent) => sliceParents.Add(parent);

    public void CheckCut()
    {
        int sliceParentsCount = 0;
        for (int idx = 0; idx < sliceParents.Count; idx++)
        {
            if (sliceParents[idx].transform.GetChild(2).gameObject.activeInHierarchy)
            {
                sliceParentsCount++;
            }
        }

        if (sliceParentsCount == totalItemsCount && !onCutDone)
        {
            TutorialManager.Instance.NextText();
            onCutDone = true;
            OnCutEnds.Invoke();
            stroveManager.SetSlicesPosition(sliceParents);
        }
    }

    Vector3 posInit;
    Vector3 rotInit;

    private void MoveBoardBack()
    {
        knife.lastObjectHit.transform.SetParent(null);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(boardCut.DOMove(posInit, 0.7f)).SetDelay(0.2f);
        sequence.Append(boardCut.DORotate(rotInit, 0.5f));
    }

    private void MoveBoard()
    {
        // Set parent while maintaining world position
        knife.lastObjectHit.transform.SetParent(boardCut, true);
        posInit = boardCut.position;
        rotInit = boardCut.rotation.eulerAngles;

        // Set all rigidbodies to kinematic
        foreach (var item in knife.lastObjectHit.GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = true;
            item.DOMove(Pot.Instance.cuttingPos.position+Vector3.up*0.081f, 0.5f);
        }
        if (Knife.countCuts >= Knife.recommendedCuts)
        { 
            Sequence sequence = DOTween.Sequence();
            sequence.Append(boardCut.DOMove(Pot.Instance.cuttingPos.position, 0.6f)).SetDelay(0.2f);
            sequence.Append(boardCut.DORotate(Pot.Instance.cuttingPos.rotation.eulerAngles, 0.5f));
            sequence.OnComplete(() =>
            {
                StartCoroutine(DoWaitRelease());
            });
        }
    }

    IEnumerator DoWaitRelease() 
    {
        foreach (var item in knife.lastObjectHit.GetComponentsInChildren<Rigidbody>())
        {
            yield return new WaitForSeconds(0.2f);
            item.transform.DOMove(Pot.Instance.transform.position+(Vector3.up*0.5f),0.4f).OnComplete(()=>item.isKinematic = false);
        }
        MoveBoardBack();
    }

    public void ConfigureFoodKitchenSector(Food item)
    {
        if (ingredients.Contains(item))
            currentItemsCount++;

        if (currentItemsCount == totalItemsCount && tutorialMode)
            TutorialManager.Instance.NextText();
    }
}
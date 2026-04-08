using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotHandle : MonoBehaviour,IUsable,IPickable
{
    private Pot pot;
    private PlateManager plate;
    [field:SerializeField] public UnityEvent OnUse { get; private set; }

    public bool KeepWorldPosition => throw new System.NotImplementedException();

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }

    void Start()
    {
        pot = Pot.Instance;
        plate = PlateManager.Instance;
        OnUse.AddListener(() =>
        {
            var list = new List<GameObject>();
            pot.GetFoodInsidePot().ForEach(food =>
            {
                list.Add(food);
            });
            StartCoroutine(PutPotfoodInPlate(list));
        });
    }

    private IEnumerator PutPotfoodInPlate(List<GameObject> foodInsidePot)
    {
        foreach (var food in foodInsidePot)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(food.transform.DOMove(pot.cuttingPos.position, 0.4f));
            sequence.Append(food.transform.DOMove(plate.positionsPlate.GetRandom().position, 0.4f));
            yield return new WaitForSeconds(0.1f);
        }
    }

    public GameObject PickUp(GameObject picker)
    {
        Use(gameObject);
        return gameObject;
    }

    public void Drop()
    {
        
    }

    public void SetIsPickable(bool isPickable)
    {
        
    }

    public bool IsPickable()
    {
        return pot.GetFoodInsidePot().Count>0;
    }

    public bool IsPickedUp()
    {
        return true;
    }
}

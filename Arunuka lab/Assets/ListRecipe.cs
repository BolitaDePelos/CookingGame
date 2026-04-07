using TMPro;
using UnityEngine;

public class ListRecipe : MonoBehaviour
{
    [SerializeField] private CardTask cardTask;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}


public class CardTask : MonoBehaviour
{
    [SerializeField] private TMP_Text completeCard;
    public void ShowCompleteCard()
    {
        //completeCard.SetActive(true);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostItemDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI = null;
    public void DisplayCost() => textUI.text = StoreController.Instance.CurrentItem.Price.ToString();
}

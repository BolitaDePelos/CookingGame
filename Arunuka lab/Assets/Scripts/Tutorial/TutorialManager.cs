using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private List<string> textList;
    [SerializeField]
    private TextMeshProUGUI indexText;
    public int index;

    public static TutorialManager Instance;

    public bool taskEnded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NextText();
    }

    public void NextText() {        
        indexText.text = textList[index];
            if (index < textList.Count - 1)
                index += 1;
        //if (taskEnded)
        
    }
}

using DG.Tweening;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
public class TextCutsAmount : MonoBehaviour
{
    [SerializeField] Knife _knife;
    TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _knife.onCut.AddListener(UpdateText);
        _knife.onPickUp.AddListener(Show);
        _knife.onDrop.AddListener(Hide);
        Hide();
    }
    private void Show() => _text.DOFade(1, 0.5f);
    private void Hide() => _text.DOFade(0, 0.5f);
    private void UpdateText() => _text.text = Knife.countCuts.ToString() + "/" + Knife.recommendedCuts.ToString();
}

[RequireComponent(typeof(TMP_Text))]
public class TextQuitKnife: MonoBehaviour
{
    [SerializeField] Knife _knife;
    TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _knife.onCut.AddListener(UpdateText);
        _knife.onPickUp.AddListener(Show);
        _knife.onDrop.AddListener(Hide);
        Hide();
    }
    private void Show() => _text.DOFade(1, 0.5f);
    private void Hide() => _text.DOFade(0, 0.5f);
    private void UpdateText() => _text.text = "Cortes "+Knife.countCuts.ToString() + "/" + Knife.recommendedCuts.ToString();
}
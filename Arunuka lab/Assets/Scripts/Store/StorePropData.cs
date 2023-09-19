using UnityEngine;



[CreateAssetMenu(fileName = "New Item Data", menuName = "CookingData/Item Data", order = 1)]
public class StorePropData: ScriptableObject
{
    [field: SerializeField] public string NameItem { get; private set; }
    [field:SerializeField] public Texture2D Texture { get; private set; }
    [field: SerializeField] public Material Material { get; private set; }
    public bool HasTexture => Texture != null;
    public bool HasMaterial => Material!= null;
}

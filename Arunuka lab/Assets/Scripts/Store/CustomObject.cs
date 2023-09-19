using UnityEngine;



[CreateAssetMenu(fileName = "New Customization Object", menuName = "CookingData/Customization Object", order = 1)]
public class CustomObject : ScriptableObject
{
    // set the prop name to match on unlock item
    [field: SerializeField] public string Name { get; private set; }
}

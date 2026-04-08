using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
public class RandomTransform
{
    [MenuItem("B_Extensions/Transform Modifier/Random Transform Trees %#t")]//%#e
    private static void RandomTransformModification()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects) 
        {
            obj.transform.localScale = Vector3.one * Random.Range(0.5f,2);
            var rot = obj.transform.rotation.eulerAngles;
            obj.transform.rotation = Quaternion.Euler(rot.x, Random.Range(0f,360f), rot.z);
        }
    }
}
#endif
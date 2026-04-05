using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using B_Extensions.SceneLoader;


#if UNITY_EDITOR
using UnityEditor.SceneManagement;

[CustomEditor(typeof(SceneLoader))]
public class EditorSceneLoader : Editor
{
    SceneLoader controller;

    public override void OnInspectorGUI()
    {
        controller = (SceneLoader)target;

        base.OnInspectorGUI();
    }

}
#endif

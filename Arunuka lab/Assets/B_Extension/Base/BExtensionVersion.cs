using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[InitializeOnLoad]
public static class BExtensionVersion
{
    static string versionKey = "BExtensionVersion_Shown";
    static string version = "2.1.3";
    static BExtensionVersion()
    {

        if (!SessionState.GetBool(versionKey, false))
        {
            Debug.Log($"B_Extension version {version}");
            SessionState.SetBool(versionKey, true);
        }
    }
}
#endif

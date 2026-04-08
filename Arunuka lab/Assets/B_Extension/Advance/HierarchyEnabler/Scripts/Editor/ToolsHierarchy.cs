using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace B_Extensions.HierarchyStates 
{
    public class ToolsHierarchy
    {
        [MenuItem("B_Extensions/Navigation Hierarchy/Restore Object In Hierarchy %#e")]
        private static void CallSearch()
        {
            CustomHierarchy.SearchEnablers();
        }
    }
}


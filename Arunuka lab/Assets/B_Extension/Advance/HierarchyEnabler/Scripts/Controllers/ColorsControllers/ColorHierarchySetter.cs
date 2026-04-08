using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace B_Extensions.HierarchyStates
{
    [DisallowMultipleComponent]
    public class ColorHierarchySetter : MonoBehaviour
    {
        public Color colorInHierarchy = new Color(1, 1, 1, 1);
        public void SetColor(Color newColor) => colorInHierarchy = newColor;

    }
}

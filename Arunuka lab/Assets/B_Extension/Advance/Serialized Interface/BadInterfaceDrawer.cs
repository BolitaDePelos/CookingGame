using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RequireBadInterfaceAttribute))]

public class BadInterfaceDrawer : PropertyDrawer
{
    private RequireBadInterfaceAttribute badAtribute;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        badAtribute = attribute as RequireBadInterfaceAttribute;

        System.Type requiredType = badAtribute.Type;

        EditorGUI.BeginProperty(position, label, property);

        // Obtenemos el valor actual como un `Object`
        UnityEngine.Object currentObject = property.objectReferenceValue;
        property.objectReferenceValue = EditorGUI.ObjectField(position, label, currentObject, typeof(UnityEngine.Object), true);

        // Validar que el objeto asignado implemente la interfaz requerida
        if (property.objectReferenceValue != null)
        {
            bool isValid = false;

            // Si el objeto es un GameObject, busca en sus componentes
            if (property.objectReferenceValue is GameObject gameObject)
            {
                Component[] components = gameObject.GetComponents<Component>();
                foreach (var component in components)
                {
                    if (requiredType.IsInstanceOfType(component))
                    {
                        isValid = true;
                        break;
                    }
                }
            }

            // Si no es un GameObject, verifica directamente
            else if (requiredType.IsInstanceOfType(property.objectReferenceValue))
            {
                isValid = true;
            }

            // Si no se encontró la interfaz, muestra un error y limpia el valor
            if (!isValid)
            {
                property.objectReferenceValue = null;
                Debug.LogError($"El objeto asignado no tiene un componente que implemente la interfaz {requiredType.Name}");
            }
        }

        EditorGUI.EndProperty();
    }
}
#endif
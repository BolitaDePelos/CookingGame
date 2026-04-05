using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class RequireBadInterfaceAttribute:PropertyAttribute
{
    public readonly System.Type Type;
    public string propertyName = "";
    public RequireBadInterfaceAttribute(Type type)
    {
        this.Type = type;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Multiple extensions.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Gets a random item in the list.
    /// </summary>
    public static TData GetRandom<TData>(this IEnumerable<TData> list)
    {
        IEnumerable<TData> enumerable = list as TData[] ?? list.ToArray();
        int index = Random.Range(0, enumerable.Count());
        return enumerable.ElementAt(index);
    }

    /// <summary>
    /// If the value is on the range or not.
    /// </summary>
    public static bool InRange(this Vector2 slider, float value)
    {
        return value >= slider.x && value <= slider.y;
    }
}
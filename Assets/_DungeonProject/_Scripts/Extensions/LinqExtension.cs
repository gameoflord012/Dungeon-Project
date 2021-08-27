using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class LinqExtension
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T item in source)
            action(item);
    }
}

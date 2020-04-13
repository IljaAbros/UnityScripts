using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IID {
    string ID { get; }
}

public static class IDFuncs {
    public static bool HasID(this IID obj) {
        return string.IsNullOrEmpty(obj.ID) == false;
    }

    public static bool Add<T>(this Dictionary<string, T> dictionary, T obj) where T : IID {
        if (obj.HasID() == false) { return false; }
        if (dictionary.ContainsKey(obj.ID)) { return false; }

        dictionary.Add(obj.ID, obj);
        return true;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class ListExtension {
    public static int IndexOf<T>(this T[] list, T obj) {
        if (obj == null) {
            return list.Length;
        }

        for (int i = 0; i < list.Length; i++) {
            if (obj.Equals(list[i])) {
                return i;
            }
        }
        return list.Length;
    }

    public static T GetRandom<T>(this T[] list) {
        return list[Random.Range(0, list.Length)];
    }

    public static T GetRandom<T>(this List<T> list) {
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> Shuffle<T>(this List<T> list, int seed) {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < list.Count - 1; i++) {
            int randomIndex = prng.Next(i, list.Count);
            T tempItem = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = tempItem;
        }

        return list;
    }

    public static T[] Shuffle<T>(this T[] array, int seed) {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++) {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }

    public static int GetUniqueIndex<T>(this Dictionary<int, T> dictionary) {
        int index = 0;
        while (dictionary.ContainsKey(index)) {
            index++;
        }

        return index;
    }

    public static string Log<T>(this T[] array, bool detail, bool debug = true) {
        if (array == null) {
            if (debug)
                return "array is null";
            else
                return "";
        }

        StringBuilder builder = new StringBuilder();

        if (debug) {
            builder.Append("Array of ").Append(typeof(T).Name).Append(" count: ").Append(array.Length).Append("/n");
        }

        if (detail) {
            int index = 0;
            foreach (T obj in array) {
                if (index != 0) {
                    builder.Append("\n");
                }

                builder.Append(obj.ToString());
                index++;
            }
        }

        return builder.ToString();
    }

    public static string Log<T>(this List<T> list, bool detail, bool debug = true) {
        if (list == null) {
            if (debug)
                return "list is null";
            else
                return "";
        }

        StringBuilder builder = new StringBuilder();

        if (debug) {
            builder.Append("Array of ").Append(typeof(T).Name).Append(" count: ").Append(list.Count).Append("/n");
        }

        if (detail) {
            int index = 0;
            foreach (T obj in list) {
                if (index != 0) {
                    builder.Append("\n");
                }

                builder.Append(obj.ToString());
                index++;
            }
        }

        return builder.ToString();
    }

    public static string Log<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, bool detail, bool debug = true) {
        if (dictionary == null) {
            if (debug)
                return "dictionary is null";
            else
                return "";
        }

        StringBuilder builder = new StringBuilder();
        if (debug) {
            builder.Append("Dictionary of ").Append(typeof(TValue).Name).Append(" count: ").Append(dictionary.Count).Append("/n");
        }

        if (detail) {
            int index = 0;
            foreach (KeyValuePair<TKey, TValue> pair in dictionary) {
                if (index != 0) {
                    builder.Append("\n");
                }
                builder.Append(pair.Key.ToString()).Append(" : ").Append(pair.Value.ToString());
                index++;
            }
        }

        return builder.ToString();
    }

    public static T[] Clone<T>(this T[] original, bool deep) {
        List<T> list = new List<T>();

        foreach (T obj in original) {
            T value = deep ? Funcs.AttemptClone<T>(obj) : obj;
            list.Add(value);
        }
        return list.ToArray();
    }

    public static List<T> Clone<T>(this List<T> original, bool deep) {
        List<T> list = new List<T>();

        foreach (T obj in original) {
            T value = deep ? Funcs.AttemptClone<T>(obj) : obj;
            list.Add(value);
        }
        return list;
    }

    public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> original, bool deep) {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        foreach (KeyValuePair<TKey, TValue> pair in original) {
            TValue value = deep ? Funcs.AttemptClone<TValue>(pair.Value) : pair.Value;
            dictionary.Add(pair.Key, value);
        }
        return dictionary;
    }

    public static int GetValue(this Dictionary<string, int> dictionary, string key, int defaultValue = 0) {
        if (dictionary.ContainsKey(key) == false) {
            return defaultValue;
        }
        return dictionary[key];
    }

    public static void SetValue(this Dictionary<string, int> dictionary, string key, int value, bool check = true) {
        if(check && dictionary.ContainsKey(key) == false) {
            return;
        }
        dictionary[key] = value;
    }

    public static void SetValue(this Dictionary<string, int> dictionary, string key, int value, int min, int max, bool check = true) {
        if (check && dictionary.ContainsKey(key) == false) {
            return;
        }
        dictionary[key] = Mathf.Clamp(value, min, max);
    }

    public static void ChangeValue(this Dictionary<string, int> dictionary, string key, int amount, bool check = true) {
        if(dictionary.ContainsKey(key) == false) {
            if (!check) {
                dictionary[key] = amount;
            }
            return;
        }
        dictionary[key] += amount;
    }

    public static void ChangeValue(this Dictionary<string, int> dictionary, string key, int amount, int min, int max, bool check = true) {
        if (dictionary.ContainsKey(key) == false) {
            if (!check) {
                dictionary[key] = Mathf.Clamp(amount, min, max);
            }
            return;
        }
        dictionary[key] = Mathf.Clamp(dictionary[key] + amount, min, max);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceCollection<T> : IEnumerable where T : Object {
    public ResourceCollection(string name, T[] list) {
        this._name = name;
        Load(list);
    }

    string _name;
    Dictionary<string, T> _content;

    public int Count {
        get { return _content.Count; }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator() {
        return _content.Values.GetEnumerator();
    }

    public IEnumerator<string> Keys() {
        return _content.Keys.GetEnumerator();
    }

    public T[] GetArray() {
        return _content.Values.ToArray();
    }

    public bool Contains(string key) {
        return _content.ContainsKey(key);
    }

    public bool Contains(T obj) {
        return _content.ContainsValue(obj);
    }

    public bool Add(T obj) {
        if (!Contains(obj)) {
            _content.Add(obj.name ,obj);
            return true;
        }

        return false;
    }

    public void Load(T[] list) {
        _content = new Dictionary<string, T>();

        foreach (T obj in list) {
            _content[obj.name] = obj;
        }
    }

    public T GetFirst() {
        return _content.Values.First();
    }

    public T Get(string key, bool returnFirst = false) {
        if (_content.ContainsKey(key) == false) {
            if (returnFirst) {
                return GetFirst();
            } else {
                return default(T);
            }
        }

        return _content[key];
    }

    public void Log(bool detail) {
        string text = _name + " - " + _content.Count;

        if (detail) {
            foreach (T item in _content.Values) {
                text += "\n" + item.name;
            }
        }

        Debug.Log(text);
    }
}

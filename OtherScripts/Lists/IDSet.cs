using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IDSet<T> : IEnumerable where T : IID {

    public IDSet() {
        _content = new HashSet<T>();
    }

    HashSet<T> _content;

    IEnumerator IEnumerable.GetEnumerator() {
        return _content.GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator() {
        return _content.GetEnumerator();
    }

    public int Count { get { return _content.Count; } }
    public T[] ToArray() { return _content.ToArray(); }
    public List<T> ToList() { return _content.ToList(); }
    public void Clear() { _content.Clear(); }

    public bool Add(T item) {
        return _content.Add(item);
    }

    public bool Remove(T item) {
        return _content.Remove(item);
    }

    public bool Contains(T item) {
        return _content.Contains(item);
    }

    public int IndexOf(T item) {
        return _content.ToList().IndexOf(item);
    }

    public T GetIndex(int index) {
        if(index > Count) { return default(T); }
        return _content.ToArray()[index];
    }

    public T FindWithID(string request) {
        foreach (T item in _content) {
            if (item.ID == request)
                return item;
        }
        return default(T);
    }

    public T[] FindAllWithID() {
        List<T> found = new List<T>();
        foreach (T item in _content) {
            if (item.HasID())
                found.Add(item);
        }
        return found.ToArray();
    }

    public T[] FindAllWithID(string request) {
        List<T> found = new List<T>();
        foreach (T item in _content) {
            if (item.ID == request)
                found.Add(item);
        }
        return found.ToArray();
    }

    public string GenerateID(string request) {
        int index = 0;
        string id = request;

        while (FindWithID(id) != null) {
            id = request + "_" + index;
            index++;
        }

        return id;
    }


}

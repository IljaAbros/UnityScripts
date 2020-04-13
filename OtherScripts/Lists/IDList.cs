using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IDList<T> : IEnumerable where T : IID {

    protected Dictionary<string, T> _content = new Dictionary<string, T>();
	
	public IEnumerator<string> GetKeys(){
		return _content.Keys.GetEnumerator();
	}

    IEnumerator IEnumerable.GetEnumerator() {
        return GetValues();
    }

    public IEnumerator<T> GetValues(){
		return _content.Values.GetEnumerator();
	}

    public int Count {
        get { return _content.Count; }
    }

    public bool Contains(string key) {
        return _content.ContainsKey(key);
    }

    public bool Contains(T item) {
        return _content.ContainsValue(item);
    }

    public T Get(string ID, bool returnFirstIfNull = false) {
        if(Contains(ID) == false) {
            if (returnFirstIfNull)
                return GetFirst();
            return default(T);
        }

        return _content[ID];
    }

    public T GetFirst() {
        if(Count == 0) { return default(T); }
        return _content.Values.First();
    }

    public T GetRandom() {
        return GetAll()[Random.Range(0, Count)];
    }

    public T[] GetAll() {
        return _content.Values.ToArray();
    }

    public T[] GetAll(System.Func<T, bool> func) {
        return _content.Values.Where(func).ToArray();
    }

    public List<T> GetList() {
        return _content.Values.ToList();
    }

    // todo: can become buggy when deleting templates at the moment
    public bool Edit(T item) {
        if (item == null) { return false; }
        if (item.ID.Empty()) { return false; }

        _content[item.ID] = item;
        return true;
    }

    public bool Remove(T item) {
        if (item == null) { return false; }
        if (!Contains(item.ID)) { return false; }

        _content.Remove(item.ID);
        return true;
    }

    public string GenerateID(string request) {
        int index = 0;
        string id = request;

        while (Contains(id)) {
            id = request + "_" + index;
            index++;
        }

        return id;
    }

    public void Log(bool detail) {
        Debug.Log(GetLog(detail));
    }

    public virtual string GetLog(bool detail) {
        string text = (typeof(T).Name) + "s Count: "+Count.ToString();

        if (detail) {
            foreach (T item in _content.Values) {
                text += "\n" + item.ID;
            }
        }

        return text;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentList<T> : MonoBehaviour where T : MonoBehaviour {

    [SerializeField]
    protected Transform _container;
    public T prefab;

    [SerializeField]
    protected List<T> components = new List<T>();

    public bool pooling;
    [SerializeField]
    protected List<T> pool = new List<T>();

    public int Count {
        get { return components.Count; }
    }

    // Start is called before the first frame update
    void Awake(){
        Clear(true);
        if(_container == null) { _container = transform; }
    }

    public T Add(System.Action<T> function) {
        T obj = GetNext();
        if (function != null) {
            function(obj);
        }
        return obj;
    }

    public T GetNext() {
        if (!pooling) {
            return Create();
        } else {
            int index = components.Count;
            if (index >= pool.Count) {
                return Create();
            } else {
                T obj = pool[index];
                PullFromPool(obj);
                components.Add(obj);
                return obj;
            }
        }
    }

    protected virtual void PullFromPool(T obj) {
        obj.gameObject.SetActive(true);
    }

    T Create() {
        T obj = Instantiate(prefab);
        obj.transform.SetParentAndReset(_container);
        components.Add(obj);
        if (pooling) {
            pool.Add(obj);
        }
        return obj;
    }

    public void Set(params System.Action<T>[] functions) {
        Clear();

        foreach (System.Action<T> function in functions) {
            T obj = Add(function);
        }
    }

    public void Populate<D>(D[] list, System.Action<T, D> function) {
        Clear();

        foreach(D data in list) {
            T obj = GetNext();
            if (function != null) {
                function(obj, data);
            }
        }
    }

    public void Clear(bool force = false) {
        if (pooling && !force) {
            _container.SetChildrenActive(false);
        } else {
            _container.DestroyChildren();
        }

        components.Clear();
    }
    
}

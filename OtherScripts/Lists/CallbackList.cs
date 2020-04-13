using System.Collections;
using System.Collections.Generic;

public class CallbackList<T> : IEnumerable, ICallbackList<T> {
	
	List<T> _list = new List<T>();
    public event System.Action<T> OnEdit;
    public event System.Action<T> OnRemoved;
    public event System.Action OnClear;
	
	public T this[int index] {
        get { return _list[index]; }
        set { 
			if(_list[index].Equals(value) )
				return;
			
			_list[index] = value;
			
			if(OnEdit != null)
				OnEdit(value);
		}
    }

    public IEnumerator GetEnumerator() {
        return _list.GetEnumerator();
    }
	
	public bool Contains(T item){
		return _list.Contains(item);
	}
	
	public bool Add(T item){
		if(Contains(item) == true)
			return false;

        _list.Add(item);

		if(OnEdit != null)
			OnEdit(item);
		return true;
	}
	
	public bool Remove(T item){
		if(Contains(item) == false)
			return false;
		
		_list.Remove(item);

		if(OnRemoved != null)
            OnRemoved(item);

		return true;
	}

    public void Clear() {
        _list.Clear();

        if (OnClear != null)
            OnClear();
    }

    public int Count { get { return _list.Count; } }

    public T GetIndex(int index) {
        if(index >= Count) { return default(T); }
        return _list[index];
    }

    public void Set(List<T> other) {
        Clear();

        foreach (T item in other) {
            Add(item);
        }
    }

    public void Set(CallbackList<T> other) {
        Clear();

        foreach (T item in other) {
            Add(item);
        }
    }

    public void Set(T[] other) {
        Clear();

        foreach (T item in other) {
            Add(item);
        }
    }

    public T[] ToArray() {
        return _list.ToArray();
    }
}

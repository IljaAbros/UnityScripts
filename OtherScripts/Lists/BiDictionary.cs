using System.Collections;
using System.Collections.Generic;

public class BiDictionary<T1, T2> : IEnumerable {
	private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
	private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();
	
	public BiDictionary(){
		this.Forward = new Indexer<T1, T2>(_forward);
		this.Reverse = new Indexer<T2, T1>(_reverse);
	}
	
	public Indexer<T1, T2> Forward { get; private set; }
	public Indexer<T2, T1> Reverse { get; private set; }
	
    public int Count {
        get { return _forward.Count; }
    }

	public void Add(T1 t1, T2 t2){
		_forward.Add(t1, t2);
		_reverse.Add(t2, t1);
	}

    public void Remove(T1 t1) {
        T2 v = _forward[t1];

        _forward.Remove(t1);
        _reverse.Remove(v);
    }

    public void Remove(T1 t1, T2 t2){
		_forward.Remove(t1);
		_reverse.Remove(t2);
	}
	
	IEnumerator IEnumerable.GetEnumerator(){
		return GetEnumerator();
	}
	
	public IEnumerator<KeyValuePair<T1,T2>> GetEnumerator(){
		return _forward.GetEnumerator();
	}

    public IEnumerator<T1> GetKeys() {
        return _forward.Keys.GetEnumerator();
    }

    public IEnumerator<T2> GetValues() {
        return _forward.Values.GetEnumerator();
    }

    public class Indexer<T3, T4> {
		private readonly Dictionary<T3,T4> _dictionary;
		
		public Indexer(Dictionary<T3,T4> dictionary){
			_dictionary = dictionary;
		}
		
		public T4 this[T3 index]{
			get { return _dictionary[index]; }
			set { _dictionary[index] = value; }
		}
		
		public bool Contains(T3 key){
			return _dictionary.ContainsKey(key);
		}

        public IEnumerator GetEnumerator() {
            return _dictionary.GetEnumerator();
        }
	} 
	
}

public static class BiDictionaryFuncs {
    public static void Add(this BiDictionary<int, string> dictionary, string value) {
        dictionary.Add(dictionary.Count, value);
    }
}
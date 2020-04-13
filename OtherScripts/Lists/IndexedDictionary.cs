using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IndexedDictionary<T>  : IEnumerable {
    public IndexedDictionary(BiDictionary<int, string> indexes) {
        _indexes = indexes;
        _content = new Dictionary<int, T>();
    }

	BiDictionary<int, string> _indexes;
	Dictionary<int, T> _content;
	
	public IEnumerator<string> GetKeys(){
		return _indexes.GetValues();
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
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClonaable<T> {
    T Clone();
}
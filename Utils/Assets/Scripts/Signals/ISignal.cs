using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignal<T> {

    void Add(Action<T> action);
    void Remove(Action<T> action);

}

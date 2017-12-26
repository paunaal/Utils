using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Signal<T>: ISignal<T> {

    HashSet<Action<T>> actions = new HashSet<Action<T>>();

    public void Add(Action<T> action)
    {

        actions.Add(action);
    }

    public void Remove(Action<T> action)
    {
        actions.Remove(action);
    }

    public void Dispatch(T obj) {
        foreach (Action<T> action in actions)
            action.Invoke(obj);
    }
}

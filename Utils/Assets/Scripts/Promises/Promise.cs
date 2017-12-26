using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promise : IPromise
{

    PromiseState state = PromiseState.Pending;

    List<Action> actions = new List<Action>();
    List<Action> onFailActions = new List<Action>();


    public void Then(Action action)
    {
        if (actions.Contains(action)) throw new Exception("Already added");

        if (state == PromiseState.Solved)
            action.Invoke();

        actions.Add(action);
    }

    public void Fail(Action action)
    {
        if (onFailActions.Contains(action)) throw new Exception("Already added");

        if (state == PromiseState.Failed)
            action.Invoke();

        onFailActions.Add(action);
    }

    public IPromise Pipe(Func<IPromise> action)
    {
        var p = new Promise();

        this.Then(()=>
        {
            var newPromise = action();
            newPromise.Then(p.Resolve);
            newPromise.Fail(p.Reject);
        });

        this.Fail(p.Reject);


        return p;
    }

    public void Resolve() {
        if (state == PromiseState.Pending) {
            state = PromiseState.Solved;

            foreach (Action action in actions)
                action.Invoke();
        }
    }

    public void Reject()
    {
        if (state == PromiseState.Pending)
        {
            state = PromiseState.Failed;

            foreach (Action action in onFailActions)
                action.Invoke();
        }
    }

    public static IPromise All(List<IPromise> promises) {

        Promise promise = new Promise();
        promise.Resolve();
        IPromise p2 = promise;

        foreach (IPromise p in promises)
        {
            p2 = p2.Pipe(() => { return p; });
        }
            

        return p2;
    }

}



enum PromiseState { Pending, Solved, Failed}

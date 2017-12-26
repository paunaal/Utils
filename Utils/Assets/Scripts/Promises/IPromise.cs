using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPromise  {

    void Then(Action action);

    IPromise Pipe(Func<IPromise> promise);

    void Fail(Action action);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Async : MonoBehaviour {

    static Dictionary<Promise, float> waitingPromises = new Dictionary<Promise, float>();
    static GameObject manager;

	public static IPromise Wait (float seconds) {

        if (manager == null)
            CreateManager();
        Promise p = new Promise();
        waitingPromises[p] = seconds;
        return p;
    }

    static void  CreateManager() {
        manager = new GameObject("AsyncManager");
        manager.AddComponent<Async>();
    }

    void Update()
    {

        var allPromises = waitingPromises.Keys.ToList();
            allPromises.ForEach(it =>
            {
                waitingPromises[it] -= Time.deltaTime;
                if (waitingPromises[it] <= 0)
                {
                    it.Resolve();
                    waitingPromises.Remove(it);
                }
            });

        /*
        int i = 0;
        while (i < waitingPromises.Keys.Count) {

            Promise p = waitingPromises.Keys.ToList()[i];
            waitingPromises[p] -= Time.deltaTime;

            if (waitingPromises[p] <= 0)
            {
                p.Resolve();
                waitingPromises.Remove(p);
            }
            else
                i++;
        }*/
    }


}

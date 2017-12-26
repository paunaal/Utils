using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPromise : MonoBehaviour {

    Image panel;
    Button button;

    // Use this for initialization
    void Start()
    {
        Promise p = new Promise();
        panel = FindObjectOfType<Image>();
        button = FindObjectOfType<Button>();

        button.onClick.AddListener(p.Resolve);
        p.Then(()=> {
            ChangeColor();
            Async.Wait(5f).Then(ChangeColor);
        });
    }

    IPromise TestPromisesAll() {
        List<IPromise> promises = new List<IPromise>();
        promises.Add(Wait());
        promises.Add(ChangeColorGradually());

        return Promise.All(promises);
    }


    void ChangeColor()
    {
        panel.color = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
        Debug.Log("Change color");

    }

    IPromise ChangeColorGradually()
    {
        Promise p = new Promise();

        StartCoroutine(SetColor(p));
        return p;
    }

    IPromise Wait()
    {
        Promise p = new Promise();

        StartCoroutine(Wait(p));
        return p;

    }

    private IEnumerator SetColor(Promise p)
    {
        for (int i = 0; i < 3; i++)
        {
            ChangeColor();
            yield return new WaitForSeconds(1);
        }
        Debug.Log("End set color");
        p.Resolve();
    }

    private IEnumerator Wait(Promise p)
    {
        yield return new WaitForSeconds(5);
        p.Resolve();
        Debug.Log("End wait");

    }
}

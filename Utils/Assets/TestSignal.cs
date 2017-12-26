using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSignal : MonoBehaviour {

    Signal<string> OnButtonPressed;
    Image panel;
    Button button;

    // Use this for initialization
    void Start () {
        OnButtonPressed = new Signal<string>();

        OnButtonPressed.Add(ChangeColor);

        panel = FindObjectOfType<Image>();
        button = FindObjectOfType<Button>();

        button.onClick.AddListener( ()=>OnButtonPressed.Dispatch("test"));
	}


    void ChangeColor(string text) {
        panel.color = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
        Debug.Log(text);
    }
	
}

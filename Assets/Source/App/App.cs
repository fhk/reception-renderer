using ReceptionRenderer.Input.Events;
using ReceptionRenderer.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    public void Awake()
    {
        this.Subscribe<KeyDownEvent>(OnKeyDown);
    }

    private void OnKeyDown(KeyDownEvent data)
    {
        if (data.Key == KeyCode.Escape)
            Application.Quit();
    }
}

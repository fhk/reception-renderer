using ReceptionRenderer.Input.Events;
using ReceptionRenderer.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputListener : MonoBehaviour
{
    public void Awake()
    {
        Poll();
    }

    public void Update()
    {
        Poll();
    }

    private void Poll()
    {
        PollKeyDown();
        PollKeyUp();
    }

    private void PollKeyDown()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            this.Publish(new KeyDownEvent(KeyCode.Escape));
    }

    private void PollKeyUp()
    {
        if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
            this.Publish(new KeyUpEvent(KeyCode.Escape));
    }
}

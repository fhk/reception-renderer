using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct KeyUpEvent
    {
        public KeyCode Key { get; set; }

        public KeyUpEvent(KeyCode key)
        {
            Key = key;
        }
    }
}
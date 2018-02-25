using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct KeyDownEvent
    {
        public KeyCode Key { get; set; }

        public KeyDownEvent(KeyCode key)
        {
            Key = key;
        }
    }
}

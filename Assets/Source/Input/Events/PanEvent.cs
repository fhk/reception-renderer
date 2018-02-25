using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct PanEvent
    {
        public Vector2 Delta { get; set; }

        public PanEvent(Vector2 delta)
        {
            Delta = delta;
        }
    }
}

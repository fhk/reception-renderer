using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct RotateEvent
    {
        public Vector2 Axis { get; set; }

        public RotateEvent(Vector2 axis)
        {
            Axis = axis;
        }
    }
}

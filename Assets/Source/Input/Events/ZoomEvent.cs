using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct ZoomEvent
    {
        public float Speed { get; set; }

        public ZoomEvent(float speed)
        {
            Speed = speed;
        }
    }
}
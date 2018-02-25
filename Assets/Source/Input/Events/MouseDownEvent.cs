using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct MouseDownEvent
    {
        public Vector2 ScreenPosition { get; set; }

        public MouseDownEvent(Vector2 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }
}

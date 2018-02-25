using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct MouseUpEvent
    {
        public Vector2 ScreenPosition { get; set; }

        public MouseUpEvent(Vector2 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }
}

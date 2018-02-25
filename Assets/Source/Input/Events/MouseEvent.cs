using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input.Events
{
    public struct MouseEvent
    {
        public Vector2 ScreenPosition { get; set; }

        public MouseEvent(Vector2 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }
}

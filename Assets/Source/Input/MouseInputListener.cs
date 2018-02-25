using ReceptionRenderer.Input.Events;
using ReceptionRenderer.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Input
{
    public class MouseInputListener : MonoBehaviour
    {
        private bool _lefDown;
        private bool _leftUp;
        private bool _middle;
        private bool _right;
        private float _scroll;
        private Vector2 _previousMouse;
        private Vector2 _currentMouse;
        private Vector2 _axis;

        public void Awake()
        {
            Poll();
        }

        public void Update()
        {
            Poll();
            PollMouseDown();
            PollMouse();
            PollMouseUp();
            PollZoom();
            PollPan();
            PollRotate();
        }
        
        private void Poll()
        {
            _lefDown = UnityEngine.Input.GetMouseButtonDown(0);
            _leftUp = UnityEngine.Input.GetMouseButtonUp(0);
            _right = UnityEngine.Input.GetMouseButton(1);
            _middle = UnityEngine.Input.GetMouseButton(2);
            _scroll = UnityEngine.Input.mouseScrollDelta.y / 10;
            _previousMouse = _currentMouse;
            _currentMouse = UnityEngine.Input.mousePosition;
            _axis.x = UnityEngine.Input.GetAxis("Mouse X");
            _axis.y = UnityEngine.Input.GetAxis("Mouse Y");
        }

        private void PollMouseDown()
        {
            if (_lefDown)
                this.Publish(new MouseDownEvent(UnityEngine.Input.mousePosition));
        }

        private void PollMouse()
        {
            this.Publish(new MouseEvent(UnityEngine.Input.mousePosition));
        }

        private void PollMouseUp()
        {
            if (_leftUp)
                this.Publish(new MouseUpEvent(UnityEngine.Input.mousePosition));
        }

        private void PollZoom()
        {
            if (_scroll != 0)
                this.Publish(new ZoomEvent(_scroll));
        }

        private void PollPan()
        {
            if (_middle)
                this.Publish(new PanEvent(_currentMouse - _previousMouse));
        }

        private void PollRotate()
        {
            if (_right)
                this.Publish(new RotateEvent(_axis));
        }
    }
}

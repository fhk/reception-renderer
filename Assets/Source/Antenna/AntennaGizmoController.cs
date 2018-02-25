using ReceptionRenderer.Input.Events;
using ReceptionRenderer.PubSub;
using ReceptionRenderer.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ReceptionRenderer.Antenna
{
    public class AntennaGizmoController : MonoBehaviour
    {
        private enum Drag
        {
            None,
            Forward,
            Right,
            Up
        }

        private Antenna _selected;
        private Drag _drag;
        private Vector3 _dragOffset;
        private Vector3 _dragStart;

        public void Awake()
        {
            this.Subscribe<MouseDownEvent>(OnMouseDown);
            this.Subscribe<MouseEvent>(OnMouse);
            this.Subscribe<MouseUpEvent>(OnMouseUp);
        }

        public void OnDestroy()
        {
            this.Unsubscribe<MouseDownEvent>();
            this.Unsubscribe<MouseEvent>();
            this.Unsubscribe<MouseUpEvent>();
        }

        private void OnMouseDown(MouseDownEvent data)
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(data.ScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Antenna antenna = hit.transform.gameObject.GetComponentInAncestor<Antenna>();

                if (antenna != null)
                {
                    _selected?.Deselect();
                    _selected = antenna;
                    _selected.Select();

                    if (hit.transform.name == "Forward")
                        _drag = Drag.Forward;

                    if (hit.transform.name == "Right")
                        _drag = Drag.Right;

                    if (hit.transform.name == "Up")
                        _drag = Drag.Up;

                    if (_drag != Drag.None)
                    {
                        _dragStart = _selected.transform.position;
                        _dragOffset = GetDragVector(ray, _dragStart, GetDragNormal(_drag));
                    }
                }
            }
        }

        private void OnMouse(MouseEvent data)
        {
            if (_selected == null || _drag == Drag.None)
                return;

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(data.ScreenPosition);
            Vector3 drag = GetDragVector(ray, _dragStart, GetDragNormal(_drag));

            _selected.transform.position = _dragStart + drag - _dragOffset;
        }

        private void OnMouseUp(MouseUpEvent data)
        {
            _drag = Drag.None;
        }

        private Vector3 GetDragVector(Ray ray, Vector3 point, Vector3 normal)
        {
            float enter;
            Plane plane = new Plane(normal, point);
            plane.Raycast(ray, out enter);
            Vector3 drag = GetDragDirection(_drag);
            Vector3 direction = ray.GetPoint(enter) - point;
            return Vector3.Project(direction, drag.normalized);
        }

        private Vector3 GetDragDirection(Drag drag)
        {
            if (drag == Drag.Forward)
                return _selected.transform.forward;

            if (drag == Drag.Right)
                return _selected.transform.right;

            if (drag == Drag.Up)
                return _selected.transform.up;

            return Vector3.zero;
        }

        private Vector3 GetDragNormal(Drag drag)
        {
            if (drag == Drag.Forward)
                return new Vector3(0, 1, 0);

            if (drag == Drag.Right)
                return new Vector3(0, 1, 0);

            if (drag == Drag.Up)
                return new Vector3(1, 0, 0);

            return Vector3.zero;
        }
    }
}

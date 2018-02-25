using ReceptionRenderer.Antenna.Events;
using ReceptionRenderer.Input.Events;
using ReceptionRenderer.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ReceptionRenderer.Antenna
{
    public class Antenna : MonoBehaviour
    {
        public float Strength = 10;

        private GameObject _gizmo;
        private GameObject _sphere;
        private GameObject _forward;
        private GameObject _right;

        public void Awake()
        {
            _gizmo = Instantiate(Resources.Load<GameObject>("Prefabs/Gizmo"));
            _gizmo.name = "Gizmo";
            _gizmo.transform.parent = transform;
            _gizmo.transform.localPosition = Vector3.zero;

            _sphere = _gizmo.transform.Find("Sphere").gameObject;
            _sphere.SetActive(true);

            _forward = _gizmo.transform.Find("Forward").gameObject;
            _forward.SetActive(false);

            _right = _gizmo.transform.Find("Right").gameObject;
            _right.SetActive(false);
        }

        public void OnEnable()
        {
            this.Publish(new AddAntennaEvent(this));
        }

        public void OnDisable()
        {
            this.Publish(new RemoveAntennaEvent(this));
        }

        public void Select()
        {
            _forward.SetActive(true);
            _right.SetActive(true);
        }

        public void Deselect()
        {
            _forward.SetActive(false);
            _right.SetActive(false);
        }
    }
}

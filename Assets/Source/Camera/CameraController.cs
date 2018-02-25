using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReceptionRenderer.PubSub;
using ReceptionRenderer.Input.Events;

namespace ReceptionRenderer.Camera
{
    public class CameraController : MonoBehaviour
    {
        public float ZoomSensitivity = 1;
        public float RotateSensitivity = 1;
        public float PanSensitivity = 1;
        public float TranslateDampening = 1;
        public float RotateDampening = 2;

        private Vector3 _targetPos;
        private Vector3 _targetRot;

        public void Awake()
        {
            this.Subscribe<PanEvent>(OnPan);
            this.Subscribe<ZoomEvent>(OnZoom);
            this.Subscribe<RotateEvent>(OnRotate);

            Reset();
        }

        public void OnDestroy()
        {
            this.Unsubscribe<PanEvent>();
            this.Unsubscribe<ZoomEvent>();
            this.Unsubscribe<RotateEvent>();
        }

        public void Reset()
        {
            _targetPos = new Vector3(0, 50, 0);
            _targetRot = new Vector3(90, 0, 0);

            transform.position = new Vector3(0,50,0);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        public void Update()
        {
            Vector3 pos = transform.position;
            float posT = Time.deltaTime * TranslateDampening;
            transform.position = Vector3.Lerp(pos, _targetPos, posT);

            Quaternion currentRot = transform.rotation;
            float rotT = Time.deltaTime * RotateDampening;
            transform.rotation = Quaternion.Lerp(currentRot, Quaternion.Euler(_targetRot), rotT);
        }

        private void OnZoom(ZoomEvent data)
        {
            _targetPos += transform.forward * data.Speed * ZoomSensitivity;
        }

        private void OnPan(PanEvent data)
        {
            _targetPos -= transform.right * data.Delta.x * PanSensitivity;
            _targetPos -= transform.up * data.Delta.y * PanSensitivity;
        }

        private void OnRotate(RotateEvent data)
        {
            _targetRot.y += data.Axis.x * RotateSensitivity;
            _targetRot.x -= data.Axis.y * RotateSensitivity;
            _targetRot.x = ClampAngle(_targetRot.x, -80, 80);
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;

            if (angle > 360)
                angle -= 360;

            return Mathf.Clamp(angle, min, max);
        }
    }
}

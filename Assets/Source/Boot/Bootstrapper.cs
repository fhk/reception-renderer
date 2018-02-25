using ReceptionRenderer.Camera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReceptionRenderer.Boot
{
    public class Bootstrapper : MonoBehaviour
    {
        private CameraController _camera;

        public void Awake()
        {
            GameObject cameraObj = Instantiate(Resources.Load<GameObject>("Prefabs/Camera"));
            _camera = cameraObj.GetComponent<CameraController>();
            _camera.name = "Camera";
            _camera.Reset();

            SceneManager.LoadScene("City-1", LoadSceneMode.Additive);
        }
    }
}


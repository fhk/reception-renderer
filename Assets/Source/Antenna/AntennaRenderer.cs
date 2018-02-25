using ReceptionRenderer.Antenna.Events;
using ReceptionRenderer.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

// TODO implement stencil culling if needed
// http://kayru.org/articles/deferred-stencil/

namespace ReceptionRenderer.Antenna
{
    public class AntennaRenderer : MonoBehaviour
    {
        public Gradient Heatmap;
        public int HeatmapTexResolution = 512;

        private List<Antenna> _antennaBuffer;
        private CommandBuffer _cmd;
        private UnityEngine.Camera _camera;
        private Mesh _sphere;
        private Material _antennaMaterial;
        private Material _receptionMaterial;
        private Texture2D _heatmapTex;

        public void Awake()
        {
            _antennaBuffer = new List<Antenna>(100);
            _cmd = new CommandBuffer();
            _cmd.name = "Render Antennae";
            _camera = GetComponent<UnityEngine.Camera>();
            _camera.AddCommandBuffer(CameraEvent.BeforeLighting, _cmd);
            _sphere = Resources.Load<Mesh>("Meshes/Sphere");
            _antennaMaterial = new Material(Shader.Find("Hidden/Antenna"));
            _receptionMaterial = new Material(Shader.Find("Hidden/Reception"));
            _receptionMaterial.SetTexture("_Heatmap", CreateHeatmapTex());

            this.Subscribe<AddAntennaEvent>(OnAddAntenna);
            this.Subscribe<RemoveAntennaEvent>(OnRemoveAntenna);
        }

        public void LateUpdate()
        {
            //TODO only recreate buffer when the antenna buffer changes
            CreateBuffer();
        }

        public void OnDestroy()
        {
            this.Unsubscribe<AddAntennaEvent>();
            this.Unsubscribe<RemoveAntennaEvent>();

            _cmd.Release();
        }

        private void OnAddAntenna(AddAntennaEvent data)
        {
            _antennaBuffer.Add(data.Antenna);
            CreateBuffer();
        }

        private void OnRemoveAntenna(RemoveAntennaEvent data)
        {
            _antennaBuffer.Remove(data.Antenna);
            CreateBuffer();
        }

        private void CreateBuffer()
        {
            _cmd.Clear();

            int accumulationBufferID = Shader.PropertyToID("Reception Accumulation Buffer");
            _cmd.GetTemporaryRT(accumulationBufferID, -1, -1, 24, FilterMode.Bilinear, RenderTextureFormat.RFloat, RenderTextureReadWrite.Default, 1);
            _cmd.SetRenderTarget(new RenderTargetIdentifier(accumulationBufferID));
            _cmd.ClearRenderTarget(true, true, Color.black, 1.0f);
            _cmd.SetRenderTarget(new RenderTargetIdentifier(accumulationBufferID), BuiltinRenderTextureType.CameraTarget);

            //TODO cull antennae
            for (int i = 0; i < _antennaBuffer.Count; i++)
            {
                float strength = _antennaBuffer[i].Strength;
                Vector3 position = _antennaBuffer[i].transform.position;
                Quaternion rotation = Quaternion.identity;
                Vector3 scale = new Vector3(strength, strength, strength);
                Matrix4x4 trs = Matrix4x4.TRS(position, rotation, scale);

                _cmd.SetGlobalFloat("_Strength", _antennaBuffer[i].Strength);
                _cmd.DrawMesh(_sphere, trs, _antennaMaterial);
            }

            _cmd.Blit(accumulationBufferID, BuiltinRenderTextureType.GBuffer0, _receptionMaterial);
            _cmd.ReleaseTemporaryRT(accumulationBufferID);
        }

        private Texture2D CreateHeatmapTex()
        {
            Color32[] colours = new Color32[HeatmapTexResolution * 2];

            for (int i = 0; i < HeatmapTexResolution; i++)
            {
                Color32 color = Heatmap.Evaluate((float)i / (HeatmapTexResolution));
                colours[i] = color;
                colours[i + HeatmapTexResolution] = color;
            }

            Texture2D heatmapTex = new Texture2D(HeatmapTexResolution, 2, TextureFormat.ARGB32, false);
            heatmapTex.wrapMode = TextureWrapMode.Clamp;
            heatmapTex.filterMode = FilterMode.Point;
            heatmapTex.SetPixels32(colours);
            heatmapTex.Apply(false);

            return heatmapTex;
        }
    }
}

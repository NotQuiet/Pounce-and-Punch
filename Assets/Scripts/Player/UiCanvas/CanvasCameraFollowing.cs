using System;
using UnityEngine;

namespace Player.UiCanvas
{
    public class CanvasCameraFollowing : MonoBehaviour
    {
        private Camera _camera;
        private Canvas _canvas;

        private void Awake()
        {
            InitializeCamera();
            InitializeCanvas();
        }

        private void Update()
        {
            FollowCamera();
        }

        private void InitializeCamera()
        {
            _camera = Camera.main;
        }

        private void InitializeCanvas()
        {
            _canvas = gameObject.GetComponent<Canvas>();
        }

        private void FollowCamera()
        {
            if(_canvas.transform.rotation != _camera.transform.rotation)
                _canvas.transform.rotation = _camera.transform.rotation;
        }
    }
}
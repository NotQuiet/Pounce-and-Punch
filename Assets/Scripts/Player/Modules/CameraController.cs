using System.Collections;
using Interfaces.Player.States;
using Player.States;
using UnityEngine;

namespace Player.Modules
{
    public class CameraController : MonoBehaviour,
        IOnAttackInput
    {
        [SerializeField] private Transform cameraPosition;
        [SerializeField] private Transform lookTarget;
        [SerializeField] private Camera camera;
        
        private readonly float _lerpSpeed = 3f;
        private readonly float _smoothReturnSpeed = 20f;
        private readonly float _smoothChangeSpeed = 6f;
        private readonly float _aimDistance = 2f;
        private Vector3 _lookTargetStartPosition;
        
        private void Start()
        {
            _lookTargetStartPosition = lookTarget.localPosition;
        }

        public void OnAimAttack()
        {
            ChangeLookTargetPosition();
        }

        public void OnEndAimAttack()
        {
            ReturnLookTarget();
        }
        
        private void LateUpdate()
        {
            Follow();
            LookAt();
        }

        private void Follow()
        {
            Vector3 newPosition = Vector3.Lerp(camera.transform.position, cameraPosition.position,
                _lerpSpeed * Time.deltaTime);
            camera.transform.position = newPosition;
        }

        private void LookAt()
        {
            camera.transform.LookAt(lookTarget);
        }

        private void ChangeLookTargetPosition()
        {
            StartCoroutine(nameof(SmoothChangeTargetPosition));
            StopCoroutine(nameof(SmoothReturnTargetPosition));
        }

        private void ReturnLookTarget()
        {
            StopCoroutine(nameof(SmoothChangeTargetPosition));
            StartCoroutine(nameof(SmoothReturnTargetPosition));
        }

        IEnumerator SmoothChangeTargetPosition()
        {
            while (lookTarget.localPosition.z < _aimDistance)
            {
                var localPosition = lookTarget.localPosition;
                Vector3 newLookPosition = Vector3.Lerp(localPosition, 
                    new Vector3(localPosition.x, localPosition.y, _aimDistance), 
                    _smoothChangeSpeed * Time.deltaTime);
                localPosition = newLookPosition;
                lookTarget.localPosition = localPosition;
                yield return null;
            }
        }
        
        IEnumerator SmoothReturnTargetPosition()
        {
            while (lookTarget.localPosition.z > _lookTargetStartPosition.z)
            {
                var localPosition = lookTarget.localPosition;
                Vector3 newLookPosition = Vector3.Lerp(localPosition, 
                    _lookTargetStartPosition, 
                    _smoothReturnSpeed * Time.deltaTime);
                localPosition = newLookPosition;
                lookTarget.localPosition = localPosition;
                yield return null;
            }
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
        }
    }
}
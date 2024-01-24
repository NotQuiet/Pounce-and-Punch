using System.Collections;
using Cysharp.Threading.Tasks;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.Modules
{
    public class CameraController : MonoBehaviour,
        IOnAttackInput, IAttackHandlerInput
    {
        [SerializeField] private Transform cameraPosition;
        [SerializeField] private Transform lookTarget;
        [SerializeField] private Camera camera;

        private readonly float _lerpSpeed = 5f;
        private readonly float _smoothReturnSpeed = 20f;
        private readonly float _smoothChangeSpeed = 100f;
        private readonly float _aimDistance = 2f;
        private readonly float _lookTargetOffsetDistance = 120f;
        private Vector3 _lookTargetStartPosition;


        private bool _isAim;
        private void Start()
        {
            _lookTargetStartPosition = lookTarget.localPosition;
            camera.transform.position = cameraPosition.position;
        }

        public void OnAimAttack()
        {
            _isAim = true;
        }

        public void OnEndAimAttack()
        {
            _isAim = false;
            ReturnLookTarget();
        }

        private void LateUpdate()
        {
            Follow();
            LookAtTarget();
        }

        private void Follow()
        {
            Vector3 newPosition = Vector3.Lerp(camera.transform.position, cameraPosition.position,
                _lerpSpeed * Time.deltaTime);
            
            camera.transform.position = newPosition;
        }

        private void ReturnLookTarget()
        {
            ReturnLookTargetAsync();
        }

        private async void ReturnLookTargetAsync()
        {
            while (!_isAim)
            {
                Vector3 newPosition = Vector3.Lerp(lookTarget.localPosition, _lookTargetStartPosition,
                    _smoothReturnSpeed * Time.deltaTime);
                lookTarget.localPosition = newPosition;
                await UniTask.Yield();
            }
           
        }

        private void LookAtTarget()
        {
            camera.transform.LookAt(lookTarget);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
        }

        public void OnAttackHandlerInput(Vector2 handlerPosition)
        {
            var newPosition = 
                new Vector3(handlerPosition.x, 0f, handlerPosition.y) / _lookTargetOffsetDistance * _aimDistance;

            Vector3 newLookPosition = Vector3.Lerp(lookTarget.localPosition,
                newPosition,
                _smoothChangeSpeed * Time.deltaTime);
            
            lookTarget.localPosition = newLookPosition;
        }
    }
}
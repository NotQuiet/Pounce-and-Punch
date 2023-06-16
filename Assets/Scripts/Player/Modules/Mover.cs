using System;
using Game;
using Interfaces.Player;
using Interfaces.Player.States;
using Player.States;
using UnityEngine;

namespace Player.Modules
{
    public class Mover : MonoBehaviour, IMove, IRotate, IOnAttackInput
    {
        [SerializeField] private Transform targetObj;

        private PlayerStateManager _stateManager;
        private MoveInput _moveInput;
        private Rigidbody _rb;
        private readonly float _speed = 10f;
        private readonly float _slowdownSpeedThreshold = 7f;
        private readonly float _slowDownOffset = 3f;
        private readonly float _turnSpeed = 0.1f;

        private float _currentSpeed = 10f;
        private float _turnSmoothVelocity;

        private bool _onAim;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _moveInput = GetComponent<MoveInput>();
        }

        private void OnEnable()
        {
            _moveInput.OnMoveInput += MoveAndRotate;
        }

        private void OnDisable()
        {
            _moveInput.OnMoveInput -= MoveAndRotate;
        }

        private void MoveAndRotate(Vector3 newPosition)
        {
            if (newPosition == Vector3.zero) return;

            ((IRotate)this).Rotate(newPosition);
            ((IMove)this).Move(newPosition);
        }

        void IMove.Move(Vector3 direction)
        {
            if (_onAim)
            {
                if (_currentSpeed > _slowdownSpeedThreshold)
                {
                    _currentSpeed -= Time.deltaTime * _slowDownOffset;
                }
            }
            else
            {
                _currentSpeed = _speed;
            }
            
            _rb.AddForce(direction * _currentSpeed * Time.deltaTime, ForceMode.Impulse);
        }

        void IRotate.Rotate(Vector3 direction)
        {
            if (_onAim) return;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(targetObj.eulerAngles.y, targetAngle,
                    ref _turnSmoothVelocity,
                    _turnSpeed);
                targetObj.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }

        public void OnAimAttack()
        {
            _onAim = true;
        }

        public void OnEndAimAttack()
        {
            _onAim = false;
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            _stateManager = manager;
        }
    }
}
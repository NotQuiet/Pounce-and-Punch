using Interfaces.Player;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using ThirdParty.Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace Player.Modules
{
    public class Mover : MonoBehaviour, IMove, IRotate, IOnAttackInput, IOnMoveInput
    {
        [SerializeField] private MovementFixedJoystick movementFixedJoystick;
        [SerializeField] private Transform targetObj;

        private PlayerStateManager _stateManager;
        // private MoveInput _moveInput;
        private Rigidbody _rb;
        private readonly float _speed = 10f;
        private readonly float _slowdownSpeedThreshold = 7f;
        private readonly float _slowDownOffset = 3f;
        private readonly float _turnSpeed = 0.1f;
        
        private float _currentSpeed = 10f;
        private float _turnSmoothVelocity;

        private bool _onAim;

        private Vector3 _moveDirection;
        private bool _isMoving;
        

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            // _moveInput = GetComponent<MoveInput>();
        }

        private void OnEnable()
        {
            //_moveInput.OnMoveInput += MoveAndRotate;
            movementFixedJoystick.OnMoveInput += OnJoystickInput;
        }

        private void OnDisable()
        {
            //_moveInput.OnMoveInput -= MoveAndRotate;
            movementFixedJoystick.OnMoveInput -= OnJoystickInput;

        }

        private void FixedUpdate()
        {
            if(_isMoving) MoveAndRotate(_moveDirection);
        }

        private void OnJoystickInput(Vector3 inputDir)
        {
            var newPosition = new Vector3(inputDir.x, 0, inputDir.y).normalized;

            _moveDirection = newPosition;
            //MoveAndRotate(newPosition);
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
                    _currentSpeed -= Time.fixedDeltaTime * _slowDownOffset;
                }
            }
            else
            {
                _currentSpeed = _speed;
            }
            
            _rb.AddForce(direction * (_currentSpeed * Time.fixedDeltaTime), ForceMode.Impulse);
        }

        void IRotate.Rotate(Vector3 direction)
        {
            if (_onAim) return;

            if (!(direction.magnitude >= 0.1f)) return;
            
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(targetObj.eulerAngles.y, targetAngle,
                ref _turnSmoothVelocity,
                _turnSpeed);
            targetObj.rotation = Quaternion.Euler(0f, angle, 0f);
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

        public void OnMove()
        {
            _isMoving = true;
        }

        public void OnEndMove()
        {
            _isMoving = false;
        }
    }
}
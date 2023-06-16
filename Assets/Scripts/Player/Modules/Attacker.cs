using Game;
using Interfaces.Player;
using Interfaces.Player.States;
using Player.States;
using ThirdParty.Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace Player.Modules
{
    public class Attacker : MonoBehaviour, IRotate, 
        IOnAttackInput
    {
        [SerializeField] private Transform targetObj;
        [SerializeField] private FixedJoystick attackJoystick;
        
        private readonly float _rotateSpeed = 0.03f;
        private float _turnSmoothVelocity;
        
        void IOnAttackInput.OnAimAttack()
        {
            attackJoystick.OnAttackInput += Rotate;
        }

        void IOnAttackInput.OnEndAimAttack()
        {
            attackJoystick.OnAttackInput -= Rotate;
        }

        public void Rotate(Vector3 direction)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(targetObj.eulerAngles.y, targetAngle,
                ref _turnSmoothVelocity,
                _rotateSpeed);
            targetObj.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            
        }
    }
}
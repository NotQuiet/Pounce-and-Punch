using System;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using ThirdParty.Joystick_Pack.Scripts.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThirdParty.Joystick_Pack.Scripts.Joysticks
{
    public class FixedJoystick : Joystick, IPlayerState
    {
        public event Action<Vector3> OnAttackInput;
        private PlayerStateManager _stateManager;

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            base.HandleInput(magnitude, normalised, radius, cam);

            OnAttackInput?.Invoke(Direction);
            _stateManager.OnAttackHandlerInput(HandlerPosition);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            _stateManager.StartAttackInput();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _stateManager.EndAttackInput();
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            _stateManager = manager;
        }
    }
}
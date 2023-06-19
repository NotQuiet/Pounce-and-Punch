using System.Collections.Generic;
using System.Linq;
using Interfaces.Player;
using Interfaces.Player.States;
using UnityEngine;

namespace Player.Matchmaking.Managers
{
    public class PlayerStateManager : MonoBehaviour, IPlayerManagersInitializer
    {
        private List<IPlayerState> _playerStates = new();

        public void AddState(IPlayerState playerState)
        {
            _playerStates.Add(playerState);
        }

        public void StartAttackInput()
        {
            foreach (var state in _playerStates)
            {
                if(state is IOnAttackInput attack)
                    attack.OnAimAttack();
            }
        }

        public void EndAttackInput()
        {
            foreach (var state in _playerStates)
            {
                if(state is IOnAttackInput attack)
                    attack.OnEndAimAttack();
            }
        }

        public void StartMoveInput()
        {
            foreach (var state in _playerStates)
            {
                if(state is IOnMoveInput move)
                    move.OnMove();
            }
        }

        public void EndMoveInput()
        {
            foreach (var state in _playerStates)
            {
                if(state is IOnMoveInput move)
                    move.OnEndMove();
            }
        }

        public void OnGetDamage(float damageValue)
        {
            foreach (var state in _playerStates)
            {
                if(state is IOnGetDamage damageState)
                    damageState.OnGetDamage(damageValue);
            }
        }

        public void OnWeaponReload(float currentValue)
        {
            foreach (var state in _playerStates)
            {
                if(state is IWeaponReload weaponReload)
                    weaponReload.OnWeaponReload(currentValue);
            }
        }

        public void OnAttackHandlerInput(Vector2 handlerPosition)
        {
            foreach (var state in _playerStates)
            {
                if(state is IAttackHandlerInput handlerInput)
                    handlerInput.OnAttackHandlerInput(handlerPosition);
            }
        }
        
        private void InitializeStates()
        {
            _playerStates = GetComponentsInChildren<IPlayerState>().ToList();

            foreach (var state in _playerStates)
            {
                state.InitializeStateManager(this);
            }
        }

        public void InitializeManager()
        {
            InitializeStates();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Player.States;
using UnityEngine;

namespace Player.States
{
    public class PlayerStateManager : MonoBehaviour
    {
        private List<IPlayerState> _playerStates = new();

        private void Awake()
        {
            InitializeStates();
        }

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
        
        private void InitializeStates()
        {
            _playerStates = GetComponentsInChildren<IPlayerState>().ToList();

            foreach (var state in _playerStates)
            {
                state.InitializeStateManager(this);
            }
        }
    }
}
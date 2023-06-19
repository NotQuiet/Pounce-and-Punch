using System;
using Interfaces.Player;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Player.Matchmaking
{
    public class DamageController : MonoBehaviour, IDamageable, IPlayerState
    {
        private PlayerStateManager _playerStateManager;
        
        public void DoDamage(int damageValue)
        {
            Debug.Log("On get damage: " + damageValue);
            _playerStateManager.OnGetDamage(damageValue);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            _playerStateManager = manager;
        }
    }
}
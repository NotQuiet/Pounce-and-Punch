using System;
using System.Globalization;
using CartoonFX;
using Interfaces.Player;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Player.Matchmaking
{
    public class DamageController : MonoBehaviour, IDamageable, IPlayerState
    {
        [SerializeField] private CFXR_ParticleText particleText;
        
        private PlayerStateManager _playerStateManager;

        public void DoDamage(float damageValue)
        {
            Debug.Log("On get damage: " + damageValue);
            
            particleText.UpdateText(damageValue.ToString(CultureInfo.InvariantCulture));
            particleText.GetComponent<ParticleSystem>().Play();
            _playerStateManager.OnGetDamage(damageValue);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            _playerStateManager = manager;
        }
    }
}
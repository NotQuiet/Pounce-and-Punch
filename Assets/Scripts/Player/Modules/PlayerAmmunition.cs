using System;
using Fabrics;
using Interfaces.Player.States;
using Player.States;
using UnityEngine;

namespace Player.Modules
{
    public class PlayerAmmunition : MonoBehaviour, IPlayerState
    {
        [SerializeField] private WeaponFabric weaponFabric;
        [SerializeField] private Transform weaponHolder;

        private PlayerStateManager _playerStateManager;
        
        private void Start()
        {
            SetWeapon();
        }

        private void SetWeapon()
        {
            var weapon = weaponFabric.Produce(weaponHolder);
            weapon.SetAmmunition(transform);
            _playerStateManager.AddState(weapon as IPlayerState);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            _playerStateManager = manager;
        }
    }
}
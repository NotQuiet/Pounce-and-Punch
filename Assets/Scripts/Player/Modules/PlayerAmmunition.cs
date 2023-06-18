using System;
using Ammunition.Weapons;
using Fabrics;
using Interfaces.Player;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Player.Modules
{
    public class PlayerAmmunition : MonoBehaviour, IPlayerState, IPlayerWeapon
    {
        [SerializeField] private WeaponFabric weaponFabric;
        [SerializeField] private Transform weaponHolder;

        private PlayerStateManager _playerStateManager;
        private PlayerWeaponManager _weaponManager;

        private Weapon _weapon;

        private void SetWeapon()
        {
            
        }

        private void InitializeWeapon()
        {
            _weapon = weaponFabric.Produce(weaponHolder);
            _weaponManager.InitializeWeapon(ref _weapon.characteristics);
            _weapon.InitializeWeaponManager(_weaponManager);
            _weapon.SetAmmunition(transform);
            _playerStateManager.AddState(_weapon as IPlayerState);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            _playerStateManager = manager;
        }

        public void InitializeWeaponManager(PlayerWeaponManager manager)
        {
            _weaponManager = manager;
            InitializeWeapon();
            SetWeapon();
        }
    }
}
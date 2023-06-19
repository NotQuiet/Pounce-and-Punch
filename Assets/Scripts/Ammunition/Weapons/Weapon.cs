using System;
using Ammunition.Shells;
using Fabrics;
using Interfaces.Player;
using Player.Matchmaking.Managers;
using Pools;
using UnityEngine;

namespace Ammunition.Weapons
{
    public class Weapon : MonoBehaviour, IWeaponPowerChange, IOnReloadObserver
    {
        public WeaponCharacteristics characteristics;

        [SerializeField] protected AbstractFabric<Shell> shellFabric;
        [SerializeField] protected Transform muzzle;
        [SerializeField] protected int poolSize;

        private Shell shell;
        private ObjectPool<Shell> _shellPool;

        private Transform _ammunition;
        private PlayerWeaponManager _weaponManager;

        private bool _isReloading;

        private void Start()
        {
            CreatePool();
            _weaponManager.AddState(this);
        }

        public void SetAmmunition(Transform ammunition)
        {
            _ammunition = ammunition;
        }

        private void CreatePool()
        {
            _shellPool = new ObjectPool<Shell>(poolSize, shellFabric, muzzle);
        }

        protected void MakeShot(bool userPower = true)
        {
            if(_isReloading)
                return;
            
            ProduceProjectile();
            Shoot(userPower);
        }
        
        private void Shoot(bool usePower)
        {
            shell.Reset();
            shell.transform.SetParent(_ammunition);
            var shellRb = shell.GetRb();
            shellRb.velocity = Vector3.zero;
            shellRb.AddForce(muzzle.forward * shell.shellCharacteristic.force, ForceMode.Impulse);
            
            if(usePower)
                shell.AddPower(characteristics.currentPower);
            
            shell.Activate();
            characteristics.currentPower = 0;
        }

        private void ProduceProjectile()
        {
            _shellPool.Produce();
            shell = _shellPool.Object;
        }

        public void InitializeWeaponManager(PlayerWeaponManager manager)
        {
            _weaponManager = manager;
        }

        public void OnReload(bool isReload)
        {
            _isReloading = isReload;
        }

        public void OnChangePower(int power)
        {
            characteristics.currentPower = power;
        }
    }
}
using System;
using Ammunition.Shells;
using Fabrics;
using Interfaces.Player;
using Player.Matchmaking.Managers;
using Pools;
using UnityEngine;

namespace Ammunition.Weapons
{
    public class Weapon : MonoBehaviour, IWeaponPowerChange
    {
        public WeaponCharacteristics characteristics;

        [SerializeField] protected AbstractFabric<Shell> shellFabric;
        [SerializeField] protected Transform muzzle;
        [SerializeField] protected int poolSize;

        protected Shell shell;
        protected ObjectPool<Shell> _shellPool;

        private Transform _ammunition;
        private PlayerWeaponManager _weaponManager;

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
        protected virtual void Shoot()
        {
            shell.transform.SetParent(_ammunition);
            var shellRb = shell.gameObject.GetComponent<Rigidbody>();
            shellRb.velocity = Vector3.zero;
            shellRb.AddForce(muzzle.forward * shell.shellCharacteristic.force, ForceMode.Impulse);
            shell.AddPower(characteristics.currentPower);
            shell.Activate();
            characteristics.currentPower = 0;
        }

        protected virtual void Reload()
        {
            
        }

        protected virtual void ProduceProjectile()
        {
            _shellPool.Produce();
            shell = _shellPool.Object;
        }

        public void InitializeWeaponManager(PlayerWeaponManager manager)
        {
            _weaponManager = manager;
        }

        public void OnChangePower(int power)
        {
            characteristics.currentPower = power;
        }
    }
}
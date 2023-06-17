using System;
using Ammunition.Shells;
using Fabrics;
using Pools;
using UnityEngine;

namespace Ammunition.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public WeaponCharacteristics characteristics;

        [SerializeField] protected AbstractFabric<Shell> shellFabric;
        [SerializeField] protected Transform muzzle;
        [SerializeField] protected int poolSize;

        protected Shell shell;
        protected ObjectPool<Shell> _shellPool;

        private Transform _ammunition;

        private void Start()
        {
            CreatePool();
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
            shellRb.AddForce(shell.transform.forward * shell.shellCharacteristic.force, ForceMode.Impulse);
        }

        protected virtual void Reload()
        {
            
        }

        protected virtual void ProduceProjectile()
        {
            _shellPool.Produce();
            shell = _shellPool.Object;
        }
    }
}
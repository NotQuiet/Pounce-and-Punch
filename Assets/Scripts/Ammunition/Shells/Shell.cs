using System;
using System.Collections;
using Interfaces.Player;
using UnityEngine;

namespace Ammunition.Shells
{
    public class Shell : MonoBehaviour
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private ParticleSystem explodeEffect;
        [SerializeField] private Rigidbody rb;
        
        public ShellCharacteristic shellCharacteristic;
        
        private bool _activated;
        
        public void Activate()
        {
            projectile.SetActive(true);
            _activated = false;
        }

        public void AddPower(int power)
        {
            shellCharacteristic.currentamage += power;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(_activated)
                return;
            
            if (other.TryGetComponent(out IDamageable damageable))
            {
                DoDamage(damageable);
            }
            
            Explode();
        }

        private void Explode()
        {
            rb.velocity = Vector3.zero;
            explodeEffect.gameObject.SetActive(true);
            projectile.SetActive(false);
            explodeEffect.Play();
            StartLifeTime();
            _activated = true;
        }

        private void DoDamage(IDamageable damageable)
        {
            damageable.DoDamage(shellCharacteristic.currentamage);
        }

        private void StartLifeTime()
        {
            StartCoroutine(nameof(LifetimeCoroutine));
        }

        private void OnLifetimeEnd()
        {
            shellCharacteristic.currentamage = shellCharacteristic.absolutDamage;
            explodeEffect.gameObject.SetActive(false);
            gameObject.SetActive(false);
            StopCoroutine(nameof(LifetimeCoroutine));
        }
        IEnumerator LifetimeCoroutine()
        {
            var currentLifeTime = shellCharacteristic.lifeTime;
            
            while (currentLifeTime > 0)
            {
                currentLifeTime -= Time.deltaTime;

                yield return null;
            }
            
            OnLifetimeEnd();
        }
    }
}
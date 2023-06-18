using System;
using System.Collections;
using Interfaces.Player;
using UnityEngine;

namespace Ammunition.Shells
{
    public class Shell : MonoBehaviour
    {
        public ShellCharacteristic shellCharacteristic;
        
        private bool _activated;
        
        public void Activate()
        {
            _activated = false;
            StartLifeTime();
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
                damageable.DoDamage(shellCharacteristic.currentamage);
                _activated = true;
            }
        }

        private void StartLifeTime()
        {
            StartCoroutine(nameof(LifetimeCoroutine));
        }

        private void OnLifetimeEnd()
        {
            shellCharacteristic.currentamage = shellCharacteristic.absolutDamage;
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
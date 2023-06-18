using System;
using System.Collections;
using Interfaces.Player;
using UnityEngine;

namespace Ammunition.Shells
{
    public class Shell : MonoBehaviour
    {
        public ShellCharacteristic shellCharacteristic;

        public void Activate()
        {
            StartLifeTime();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.DoDamage(shellCharacteristic.damage);
            }
        }

        private void StartLifeTime()
        {
            StartCoroutine(nameof(LifetimeCoroutine));
        }

        private void OnLifetimeEnd()
        {
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
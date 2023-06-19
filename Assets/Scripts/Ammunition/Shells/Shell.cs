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
        [SerializeField] private GameObject shellGo;
        [SerializeField] private ShellDamageTrigger damageTrigger;

        public ShellCharacteristic shellCharacteristic;
        
        private bool _activated;

        private void OnEnable()
        {
            damageTrigger.OnTrigger += OnTrigger;
        }

        private void OnDisable()
        {
            damageTrigger.OnTrigger -= OnTrigger;
        }

        public virtual void Activate()
        {
            projectile.SetActive(true);
            _activated = false;
        }

        public void AddPower(int power)
        {
            Debug.Log("Add power to shell: " + power);
            shellCharacteristic.currentDamage += power;
        }

        public Rigidbody GetRb()
        {
            return rb;
        }

        public void Reset()
        {
            shellGo.transform.localPosition = Vector3.zero;
            shellGo.transform.localRotation = Quaternion.identity;
        }

        private void OnTrigger(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                DoDamage(damageable);
            }
            
            Explode();
        }
        
        protected virtual void Explode()
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
            if(_activated)
                return;

            Debug.Log("Shell damage: " + shellCharacteristic.currentDamage);
            damageable.DoDamage(shellCharacteristic.currentDamage);
            ResetDamage();
        }

        private void ResetDamage()
        {
            shellCharacteristic.currentDamage = shellCharacteristic.absolutDamage;
        }

        private void StartLifeTime()
        {
            StartCoroutine(nameof(LifetimeCoroutine));
        }

        private void OnLifetimeEnd()
        {
            shellCharacteristic.currentDamage = shellCharacteristic.absolutDamage;
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
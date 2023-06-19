using System.Collections;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Ammunition.Weapons
{
    public class LaserMachineGun : Weapon, IOnAttackInput
    {
        private bool _isActive;
        public void OnAimAttack()
        {
            StartShooting();
        }

        public void OnEndAimAttack()
        {
            Debug.Log("Shoot power: " + characteristics.currentPower);
            StopShooting();
        }

        private void StartShooting()
        {
            _isActive = true;
            StartCoroutine(nameof(Shooting));
        }

        private void StopShooting()
        {
            _isActive = false;
            StopCoroutine(nameof(Shooting));
        }

        IEnumerator Shooting()
        {
            Debug.Log("Shooting");

            while (_isActive)
            {
                MakeShot(false);

                yield return new WaitForSeconds(characteristics.shootDelay);
            }
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
        }
    }
}
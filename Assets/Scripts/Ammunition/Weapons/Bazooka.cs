using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Ammunition.Weapons
{
    public class Bazooka : Weapon, IOnAttackInput
    {
        public void OnAimAttack()
        {
            // effect here
        }

        public void OnEndAimAttack()
        {
            Debug.Log("Shoot power: " + characteristics.currentPower);
            ProduceProjectile();
            Shoot();
        }
        
        public void InitializeStateManager(PlayerStateManager manager)
        {
        }
    }
}
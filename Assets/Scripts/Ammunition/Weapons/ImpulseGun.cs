using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Ammunition.Weapons
{
    public class ImpulseGun : Weapon, IOnAttackInput
    {
        public void OnAimAttack()
        {
            // effect here
        }

        public void OnEndAimAttack()
        {
            Debug.Log("Shoot power: " + characteristics.currentPower);
            
            MakeShot();
        }
        
        public void InitializeStateManager(PlayerStateManager manager)
        {
        }
    }
}
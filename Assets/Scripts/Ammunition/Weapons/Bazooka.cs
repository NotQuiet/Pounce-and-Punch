using Interfaces.Player.States;
using Player.States;

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
            ProduceProjectile();
            Shoot();
        }
        
        public void InitializeStateManager(PlayerStateManager manager)
        {
        }
    }
}
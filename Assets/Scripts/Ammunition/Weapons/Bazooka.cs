using Interfaces.Player.States;
using Player.States;

namespace Ammunition.Weapons
{
    public class Bazooka : Weapon, IOnAttackInput
    {
        public void OnAimAttack()
        {
            ProduceProjectile();
        }

        public void OnEndAimAttack()
        {
            Shoot();
        }
        
        public void InitializeStateManager(PlayerStateManager manager)
        {
        }
    }
}
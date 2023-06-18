using Ammunition.Weapons;
using Player.Matchmaking.Managers;

namespace Interfaces.Player
{
    public interface IPlayerWeapon
    {
        void InitializeWeaponManager(PlayerWeaponManager manager);
    }

    public interface IPlayerWeaponCharacteristics : IPlayerWeapon
    {
        void InitializeWeapon(ref WeaponCharacteristics weaponCharacteristics);
    }

    public interface IWeaponPowerChange : IPlayerWeapon
    {
        void OnChangePower(int power);
    }
}
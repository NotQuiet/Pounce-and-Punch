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
        void OnChangePower(float power);
    }

    public interface IOnReloadObserver : IPlayerWeapon
    {
        void OnReload(bool isReload);
    }
}
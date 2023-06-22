using System.Collections.Generic;
using System.Linq;
using Ammunition.Weapons;
using Interfaces.Player;
using Interfaces.Player.States;
using UnityEngine;

namespace Player.Matchmaking.Managers
{
    public class PlayerWeaponManager : MonoBehaviour, IPlayerManagersInitializer
    {
        private List<IPlayerWeapon> _weaponStates = new();

        public void AddState(IPlayerWeapon state)
        {
            _weaponStates.Add(state);
        }
        public void InitializeWeapon(ref WeaponCharacteristics weaponCharacteristics)
        {
            foreach (var state in _weaponStates)
            {
                if (state is IPlayerWeaponCharacteristics characteristics)
                    characteristics.InitializeWeapon(ref weaponCharacteristics);
            }
        }

        public void PowerChange(float value)
        {
            foreach (var state in _weaponStates)
            {
                if (state is IWeaponPowerChange powerChange)
                    powerChange.OnChangePower(value);
            }
        }

        public void OnReload(bool isReload)
        {
            foreach (var state in _weaponStates)
            {
                if (state is IOnReloadObserver reload)
                    reload.OnReload(isReload);
            }
        }


        public void InitializeManager()
        {
            _weaponStates = GetComponentsInChildren<IPlayerWeapon>().ToList();

            foreach (var state in _weaponStates)
            {
                state.InitializeWeaponManager(this);
            }
        }
    }
}
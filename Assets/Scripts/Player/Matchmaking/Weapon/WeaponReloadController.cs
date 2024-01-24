using System.Collections;
using Ammunition.Weapons;
using Cysharp.Threading.Tasks;
using Interfaces.Player;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Matchmaking.Weapon
{
    public class WeaponReloadController : MonoBehaviour, IOnAttackInput, IPlayerWeaponCharacteristics
    {
        [SerializeField] private Image reloadImage;

        private WeaponCharacteristics _weaponCharacteristics;
        private PlayerWeaponManager _weaponManager;

        private bool _isAim;
        private float _currentPower = 0;

        public void OnAimAttack()
        {
            _isAim = true;
            StartReloadDecrease();
        }

        public void OnEndAimAttack()
        {
            _isAim = false;
            _currentPower = 0;
            ReloadWeapon();
        }

        private void StartReloadDecrease()
        {
            DecreaseWeaponReload();
        }

        private async void DecreaseWeaponReload()
        {
            var power = _weaponCharacteristics.possiblePower;
            
            while (_isAim)
            {
                reloadImage.fillAmount -= Time.deltaTime / power;
                
                if(reloadImage.fillAmount > 0.1f)
                    _currentPower++;
                
                _weaponManager.PowerChange(_currentPower * _weaponCharacteristics.powerMultiplier);
                
                await UniTask.WaitForFixedUpdate();
            }
        }

        private void ReloadWeapon()
        {
            ReloadWeaponAsync();
        }

        private async void ReloadWeaponAsync()
        {
            var power = _weaponCharacteristics.possiblePower;

            while (!_isAim)
            {
                reloadImage.fillAmount += Time.deltaTime / power;

                await UniTask.WaitForFixedUpdate();
            }
        }
        
        
        public void InitializeStateManager(PlayerStateManager manager)
        {
        }

        public void InitializeWeaponManager(PlayerWeaponManager manager)
        {
            _weaponManager = manager;
        }

        public void InitializeWeapon(ref WeaponCharacteristics weaponCharacteristics)
        {
            _weaponCharacteristics = weaponCharacteristics;
        }
    }
}
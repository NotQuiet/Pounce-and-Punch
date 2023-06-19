using System.Collections;
using Ammunition.Weapons;
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
        private int currentPower = 0;

        public void OnAimAttack()
        {
            _isAim = true;
            StartReloadDecrease();
        }

        public void OnEndAimAttack()
        {
            _isAim = false;
            ReloadWeapon();
            
            NeedReload(false);
        }

        private void StartReloadDecrease()
        {
            StopCoroutine(nameof(ReloadWeaponUi));
            StartCoroutine(nameof(DecreaseWeaponReloadUi));
        }

        IEnumerator DecreaseWeaponReloadUi()
        {
            var power = _weaponCharacteristics.possiblePower;
            while (_isAim)
            {
                if (reloadImage.fillAmount <= 0.01f)
                {
                    NeedReload(true);
                    ReloadWeapon();
                }

                reloadImage.fillAmount -= Time.deltaTime / power;
                
                if(reloadImage.fillAmount > 0.1f)
                    currentPower++;
                
                _weaponManager.PowerChange(currentPower);
                yield return null;
            }
        }

        private void ReloadWeapon()
        {
            StopCoroutine(nameof(DecreaseWeaponReloadUi));
            StartCoroutine(nameof(ReloadWeaponUi));
        }

        private void StopReload()
        {
            StopCoroutine(nameof(ReloadWeaponUi));
        }
        
        IEnumerator ReloadWeaponUi()
        {
            var power = _weaponCharacteristics.possiblePower;

            while (true)
            {
                if (_isAim && reloadImage.fillAmount >= 0.01f)
                {
                    NeedReload(false);
                    StartReloadDecrease();
                }
                
                reloadImage.fillAmount += Time.deltaTime / power;

                if(reloadImage.fillAmount >= 1f)
                    StopReload();
                
                yield return null;
            }
        }

        private void NeedReload(bool isNeed)
        {
            _weaponManager.OnReload(isNeed);
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
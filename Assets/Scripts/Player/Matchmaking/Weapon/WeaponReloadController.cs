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

        public void OnAimAttack()
        {
            _isAim = true;
            StartReloadDecrease();
        }

        public void OnEndAimAttack()
        {
            _isAim = false;
            ReloadWeapon();
        }

        private void StartReloadDecrease()
        {
            StopCoroutine(nameof(ReloadWeaponUi));
            StartCoroutine(nameof(DecreaseWeaponReloadUi));
        }

        IEnumerator DecreaseWeaponReloadUi()
        {
            var power = _weaponCharacteristics.possiblePower;
            int currentPower = 0;
            while (_isAim)
            {
                reloadImage.fillAmount -= Time.deltaTime / power;
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
        
        IEnumerator ReloadWeaponUi()
        {
            var power = _weaponCharacteristics.possiblePower;

            while (!_isAim)
            {
                reloadImage.fillAmount += Time.deltaTime / power;
                yield return null;
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
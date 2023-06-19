using Cysharp.Threading.Tasks;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Player.Matchmaking.PlayerLife
{
    public class PlayerLifeController : MonoBehaviour, IOnGetDamage
    {
        [SerializeField] private PlayerLifeCharacteristics playerLifeCharacteristics;
        [SerializeField] private PlayerHealthSlider playerHealthSlider;

        private PlayerStateManager _playerStateManager;
        
        private void Start()
        {
            InitializeSlider();
        }

        private async UniTask ResetPlayer()
        {
            await UniTask.Delay(3000);
            
            playerLifeCharacteristics.ChangeCurrentHp(playerLifeCharacteristics.absoluteHp);
            playerHealthSlider.SetAbsoluteValues(playerLifeCharacteristics.absoluteHp);
        }
        
        public void OnGetDamage(float damageValue)
        {
            var currentHp = playerLifeCharacteristics.CurrentHp;
            var newValue = currentHp - damageValue;
            
            playerLifeCharacteristics.ChangeCurrentHp(newValue);
            
            OnChangeHp();
            IsLethalDamage();
            
            Debug.Log($"On get damage: damage value - {damageValue} currentHp - {playerLifeCharacteristics.CurrentHp}");
        }

        private async void IsLethalDamage()
        {
            if (playerLifeCharacteristics.CurrentHp <= 0)
            {
                _playerStateManager.OnPlayerDead();
                await ResetPlayer();
            }
        }

        private void OnChangeHp()
        {
            playerHealthSlider.OnHealthChange(playerLifeCharacteristics.CurrentHp);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            _playerStateManager = manager;
            playerLifeCharacteristics.Initialize();
        }

        private void InitializeSlider()
        {
            playerHealthSlider.SetAbsoluteValues(playerLifeCharacteristics.absoluteHp);
        }
    }
}

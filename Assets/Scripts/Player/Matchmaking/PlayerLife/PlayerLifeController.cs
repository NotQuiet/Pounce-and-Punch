using System;
using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Matchmaking.PlayerLife
{
    public class PlayerLifeController : MonoBehaviour, IOnGetDamage
    {
        [SerializeField] private PlayerLifeCharacteristics playerLifeCharacteristics;
        [SerializeField] private PlayerHealthSlider playerHealthSlider;

        private void Start()
        {
            InitializeSlider();
        }
        
        public void OnGetDamage(float damageValue)
        {
            var currentHp = playerLifeCharacteristics.CurrentHp;
            var newValue = currentHp - damageValue;
            
            playerLifeCharacteristics.ChangeCurrentHp(newValue);
            
            OnChangeHp();
            
            Debug.Log($"On get damage: damage value - {damageValue} currentHp - {playerLifeCharacteristics.CurrentHp}");
        }

        private void OnChangeHp()
        {
            playerHealthSlider.OnHealthChange(playerLifeCharacteristics.CurrentHp);
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            playerLifeCharacteristics.Initialize();
            
        }

        private void InitializeSlider()
        {
            playerHealthSlider.SetAbsoluteValues(playerLifeCharacteristics.absoluteHp);
        }
    }
}

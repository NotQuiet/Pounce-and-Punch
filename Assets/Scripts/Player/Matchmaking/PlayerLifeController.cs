using Interfaces.Player.States;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Player.Matchmaking
{
    public class PlayerLifeController : MonoBehaviour, IOnGetDamage
    {
        [SerializeField] private PlayerLifeCharacteristics playerLifeCharacteristics;
        
        public void OnGetDamage(int damageValue)
        {
            var currentHp = playerLifeCharacteristics.CurrentHp;
            var newValue = currentHp - damageValue;
            
            playerLifeCharacteristics.ChangeCurrentHp(newValue);
            
            Debug.Log($"On get damage: damage value - {damageValue} currentHp - {playerLifeCharacteristics.CurrentHp}");
        }

        public void InitializeStateManager(PlayerStateManager manager)
        {
            playerLifeCharacteristics.Initialize();
        }
    }
}

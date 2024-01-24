using Player.Matchmaking.Managers;
using UnityEngine;

namespace Interfaces.Player.States
{
    public interface IPlayerState
    {
        void InitializeStateManager(PlayerStateManager manager);
    }
    
    public interface IOnAttackInput : IPlayerState
    {
        void OnAimAttack();
        void OnEndAimAttack();
    }
    
    public interface IOnMoveInput : IPlayerState
    {
        void OnMove();
        void OnEndMove();
    }

    public interface IOnGetDamage : IPlayerState
    {
        void OnGetDamage(float damageValue);
    }

    public interface IWeaponReload : IPlayerState
    {
        void OnWeaponReload(float currentValue);
    }

    public interface IAttackHandlerInput : IPlayerState
    {
        void OnAttackHandlerInput(Vector2 handlerPosition);
    }

    public interface IOnDeath : IPlayerState
    {
        void OnPlayerDeath();
    }

    public interface IOnAlive : IPlayerState
    {
        void OnAlive();
    }
}
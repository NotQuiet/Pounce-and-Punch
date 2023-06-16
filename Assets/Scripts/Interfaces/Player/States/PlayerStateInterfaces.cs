using Player.States;

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
}
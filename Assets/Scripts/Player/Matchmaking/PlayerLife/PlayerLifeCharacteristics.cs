using System;

namespace Player.Matchmaking.PlayerLife
{
    [Serializable]
    public class PlayerLifeCharacteristics
    {
        public float absoluteHp;
        public float CurrentHp { get; private set; }

        public void Initialize()
        {
            CurrentHp = absoluteHp;
        }
        
        public void ChangeCurrentHp(float newValue)
        {
            CurrentHp = newValue;
        }
    }
}
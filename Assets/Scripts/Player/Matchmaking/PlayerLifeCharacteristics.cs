using System;

namespace Player.Matchmaking
{
    [Serializable]
    public class PlayerLifeCharacteristics
    {
        public int absoluteHp;
        public int CurrentHp { get; private set; }

        public void Initialize()
        {
            CurrentHp = absoluteHp;
        }
        
        public void ChangeCurrentHp(int newValue)
        {
            CurrentHp = newValue;
        }
    }
}
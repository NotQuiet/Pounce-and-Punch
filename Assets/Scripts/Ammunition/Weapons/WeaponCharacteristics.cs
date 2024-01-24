using System;

namespace Ammunition.Weapons
{
    [Serializable]
    public class WeaponCharacteristics
    {
        public float powerMultiplier;

        public float possiblePower;
        public float currentPower;

        public float shootDelay;
    }
}
using System;

namespace Ammunition.Weapons
{
    [Serializable]
    public class WeaponCharacteristics
    {
        public float possiblePower;
        public int currentPower;

        public float shootDelay;
    }
}
using System;

namespace Ammunition.Shells
{
    [Serializable]
    public class ShellCharacteristic
    {
        public int absolutDamage;
        public float currentDamage;
        public int force;
        public float lifeTime;
    }
}
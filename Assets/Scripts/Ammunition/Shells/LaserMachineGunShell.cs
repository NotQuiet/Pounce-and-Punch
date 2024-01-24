using UnityEngine;

namespace Ammunition.Shells
{
    public class LaserMachineGunShell : Shell
    {
        [SerializeField] private ParticleSystem muzzleEffect;
        
        public override void Activate()
        {
            muzzleEffect.gameObject.SetActive(true);
            muzzleEffect.Play();
            base.Activate();
        }
    }
}
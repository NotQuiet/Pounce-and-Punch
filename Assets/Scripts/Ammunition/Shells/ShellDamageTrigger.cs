using System;
using UnityEngine;

namespace Ammunition.Shells
{
    public class ShellDamageTrigger : MonoBehaviour
    {
        public event Action<Collider> OnTrigger;
        
        private void OnTriggerEnter(Collider other)
        {
            OnTrigger?.Invoke(other);
      
        }
    }
}
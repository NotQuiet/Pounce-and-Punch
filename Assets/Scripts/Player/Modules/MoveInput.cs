using System;
using UnityEngine;

namespace Player.Modules
{
    public class MoveInput : MonoBehaviour
    {
        public event Action<Vector3> OnMoveInput;

        private void Update()
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            var newPosition = new Vector3(horizontal, 0, vertical).normalized;
            OnMove(newPosition);
        }

        private void OnMove(Vector3 newPosition)
        {
            OnMoveInput?.Invoke(newPosition);
        }
    }
}

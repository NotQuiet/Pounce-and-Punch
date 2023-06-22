using System;
using Ammunition.Weapons;
using Fabrics;
using Interfaces.Player;
using Player.Matchmaking.Managers;
using UnityEngine;

namespace Player.Initialize
{
    public class PlayerConstructor : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        public Mesh meshToSet;

        private PlayerWeaponManager _playerWeaponManager;

        
        
        private void Start()
        {
            SetModel(meshToSet);
        }

        private void SetModel(Mesh playerMesh)
        {
            meshFilter.mesh = playerMesh;
        }
    }
}
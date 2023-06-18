using System;
using System.Collections.Generic;
using System.Linq;
using Ammunition.Weapons;
using Interfaces.Player;
using UnityEngine;

namespace Player.Initialize
{
    public class PlayerInitializeManager : MonoBehaviour
    {
        private List<IPlayerManagersInitializer> _managers = new();

        private void Awake()
        {
            InitializeManagers();
        }

        private void InitializeManagers()
        {
            _managers = GetComponentsInChildren<IPlayerManagersInitializer>().ToList();

            foreach (var state in _managers)
            {
                state.InitializeManager();
            }
        }
    }
}
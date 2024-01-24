using System;
using UnityEngine;

namespace Map.Spawning
{
    [Serializable]
    public class SpawnPoint
    {
        public Team team;
        public Transform position;
    }
}
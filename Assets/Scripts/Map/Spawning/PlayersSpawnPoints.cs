using System.Collections.Generic;
using UnityEngine;

namespace Map.Spawning
{
    public enum Team
    {
        Blue, 
        Red
    }
    public class PlayersSpawnPoints : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints;
    }
}
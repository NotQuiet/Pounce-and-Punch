using System.Collections.Generic;
using UnityEngine;

namespace Map.Spawning
{
    public enum Team
    {
        Blue, 
        Red
    }
    public class PlayersSpawnController : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints;
    }
}
using System.Collections.Generic;
using Player.Initialize;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "Matchmaking/Heroes", fileName = "MatchmakingHeroesPrefabs")]
    public class MatchmakingHeroes : ScriptableObject
    {
        [SerializeField] private List<PlayerConstructor> heroes;
    }
}
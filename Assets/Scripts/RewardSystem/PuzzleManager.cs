using System;
using System.Collections.Generic;
using UnityEngine;

namespace RewardSystem
{
    // A puzzle manager class, responsible for registering spawn locations for each puzzle instance and triggering the decoupled puzzle solved event
    public class PuzzleManager : MonoBehaviour
    {
        public static event Action<PuzzleSolvedEvent> OnPuzzleSolved;
        private static Dictionary<string, Transform> _puzzleIdToSpawnLocation = new Dictionary<string, Transform>();

        public static void RegisterSpawnPos(string puzzleId, Transform spawnLocation)
        {
            _puzzleIdToSpawnLocation[puzzleId] = spawnLocation;
        }
        public static void PuzzleSolved(string puzzleId, int rewardQuantity)
        {
            Transform spawnLocation = _puzzleIdToSpawnLocation[puzzleId];
            OnPuzzleSolved?.Invoke(new PuzzleSolvedEvent(puzzleId, rewardQuantity, spawnLocation));
            PuzzleTrigger.IsPuzzleBoardActive = false;
        }
    }
}
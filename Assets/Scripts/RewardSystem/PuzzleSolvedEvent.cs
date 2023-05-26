using System;
using UnityEngine;

namespace RewardSystem
{
    // A custom event class for the puzzles to create a more decoupled reward system, each puzzle can have its own reward quantity and spawn location
    public class PuzzleSolvedEvent : EventArgs
    {
        public string PuzzleId { get; }
        public int RewardQuantity { get; }
        public Transform SpawnLocation { get; }

        public PuzzleSolvedEvent(string puzzleId, int rewardQuantity, Transform spawnLocation)
        {
            PuzzleId = puzzleId;
            RewardQuantity = rewardQuantity;
            SpawnLocation = spawnLocation;
        }
    }
}
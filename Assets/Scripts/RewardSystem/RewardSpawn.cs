using System;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RewardSystem
{
    // This class is used for positions in game that rewards can spawn - This prefab contains the spawn specific logic,
    // ideally the reward prefab and force would be sepertated out
    public class RewardSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject rewardPrefab;
        [SerializeField] private float force = 40;
        private Transform _rewardSpawnPoint;
        private String _puzzleId;

        private void Awake()
        {
            _rewardSpawnPoint = transform;
            _puzzleId = gameObject.name;
        }

        private void OnEnable()
        {
            PuzzleManager.RegisterSpawnPos(_puzzleId, _rewardSpawnPoint);
            PuzzleManager.OnPuzzleSolved += SpawnRewards;
        }

        private void OnDisable()
        {
            PuzzleManager.OnPuzzleSolved -= SpawnRewards;
        }

        private void SpawnRewards(PuzzleSolvedEvent puzzleSolvedEvent)
        {
            if (puzzleSolvedEvent.PuzzleId != _puzzleId) return;

            for (int i = 0; i < puzzleSolvedEvent.RewardQuantity; i++)
            {
                GameObject rewardTemp = Instantiate(rewardPrefab, puzzleSolvedEvent.SpawnLocation.position, Quaternion.identity);
                Rigidbody rewardRigidbody = rewardTemp.GetComponent<Rigidbody>();
                
                Vector3 forceDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                rewardRigidbody.AddForce(forceDirection * force, ForceMode.Impulse);
            }
        }
    }
}
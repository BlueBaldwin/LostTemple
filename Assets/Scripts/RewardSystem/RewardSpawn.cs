using System;
using Puzzles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RewardSystem
{
    public class RewardSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject rewardPrefab;
        [SerializeField] private bool isRewarding;
        [SerializeField] private float force;
        [SerializeField] private int rewardCount = 10;
        private Transform rewardSpawnPoint;

        private void Awake()
        {
            rewardSpawnPoint = transform;
        }

        private void OnEnable()
        {
            NumberMatchManager.OnPuzzleComplete += SpawnRewards;
        }

        private void Update()
        {
            if (isRewarding)
            {
                SpawnRewards(rewardCount);
                isRewarding = false;
            }
        }

        private void SpawnRewards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject rewardTemp = Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
                Rigidbody rewardRigidbody = rewardTemp.GetComponent<Rigidbody>();
                
                Vector3 forceDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                rewardRigidbody.AddForce(forceDirection * force, ForceMode.Impulse);
            }
        }
    }
}
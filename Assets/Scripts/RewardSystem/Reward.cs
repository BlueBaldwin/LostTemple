using System;
using UnityEditor.TestTools.CodeCoverage;
using UnityEngine;
using UnityEngine.Events;

namespace RewardSystem
{
    public class Reward : MonoBehaviour
    {
        public static Action onCollected;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Trigger event to add the score to the HUD
                onCollected?.Invoke();
                Destroy(gameObject);
                
            }
        }
    }
}
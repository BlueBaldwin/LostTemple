using System;
using UnityEngine;
using UnityEngine.Events;

namespace RewardSystem
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 50f;
        [SerializeField] private AudioClip _clip;
        
        public static Action onCollected;

        private void Update()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            // Trigger event to add the score to the HUD
            onCollected?.Invoke();
            Destroy(gameObject);
            SoundManager.Instance.PlaySound(_clip, false);
        }
    }
}
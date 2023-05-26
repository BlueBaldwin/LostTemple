using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class DialogTrigger : MonoBehaviour
    {
        [SerializeField] private AudioClip[] dialogClips;
        [SerializeField] Collider[] dialogTriggers;

        private Dictionary<Collider, AudioClip> _colliderAudioMap;
        private List<Collider> _colliderSequence;
        
        private void Start()
        {
            if (dialogClips.Length != dialogTriggers.Length)
            {
                Debug.LogError("Not all colliders or clips are assigned");
                return;
            }

            _colliderAudioMap = new Dictionary<Collider, AudioClip>();
            _colliderSequence = new List<Collider>();
            
            // PAiring them all up into a dictionary
            for (int i = 0; i < dialogTriggers.Length; i++)
            {
                _colliderAudioMap.Add(dialogTriggers[i], dialogClips[i]);
                _colliderSequence.Add(dialogTriggers[i]);
            }
            // Setting t
            dialogTriggers[3].enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_colliderAudioMap.TryGetValue(other, out var clip))
            {
                SoundManager.Instance.PlaySound(clip, false);
                _colliderAudioMap.Remove(other);
                other.enabled = false;
                
                int currentIndex = _colliderSequence.IndexOf(other);
                // Checks to make sure the collider is there and also that it's not the last one
                if (currentIndex != -1 && currentIndex + 1 < _colliderSequence.Count)
                {
                    Collider nextCollider = _colliderSequence[currentIndex + 1];
                    nextCollider.enabled = true;
                }
            }
        }
    }
}
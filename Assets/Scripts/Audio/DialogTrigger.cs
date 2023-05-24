using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class DialogTrigger : MonoBehaviour
    {
        private Dictionary<Collider, AudioClip> _colliderAudioMap;
        
        [SerializeField] private AudioClip[] dialogClips;
        [SerializeField] Collider[] dialogTriggers;

        private void Start()
        {
            if (dialogClips.Length != dialogTriggers.Length)
            {
                Debug.LogError("Not all colliders or clips are assigned");
                return;
            }

            _colliderAudioMap = new Dictionary<Collider, AudioClip>();
            
            // PAiring them all up into a dictionary
            for (int i = 0; i < dialogTriggers.Length; i++)
            {
                _colliderAudioMap.Add(dialogTriggers[i], dialogClips[i]);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_colliderAudioMap.TryGetValue(other, out var clip))
            {
                SoundManager.Instance.PlaySound(clip, false);
                _colliderAudioMap.Remove(other);
                other.enabled = false;
            }
            else
            {
                Debug.LogError("Collider not found in the dictionary");
            }
        }
    }
}
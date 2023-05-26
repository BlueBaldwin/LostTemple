using System;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

namespace Puzzles
{
    // This class handles the scale weights placement and updates the total weight
    public class Scale : MonoBehaviour
    {
        [SerializeField] private Transform weightSlot1;
        [SerializeField] private Transform weightSlot2;
        [SerializeField] private Transform weightSlot3;
        [SerializeField] private Transform weightSlot4;

        private List<Transform> _weightSlots;
        private List<Weight> _weightsOnScale;
        private int _totalWeight;

        public event Action<int> OnWeightChanged;

        private void Awake()
        {
            _weightSlots = new List<Transform>();
            _weightSlots.Add(weightSlot1);
            _weightSlots.Add(weightSlot2);
            _weightSlots.Add(weightSlot3);
            _weightSlots.Add(weightSlot4);

            _weightsOnScale = new List<Weight>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Weight weight = other.GetComponent<Weight>();
            if (weight == null || weight.IsPickedUp) return;
            weight.transform.SetParent(_weightSlots[_weightsOnScale.Count]);

            // Setting the weights position relative to it's size to get the bottom of the weight on the scale

            Vector3 offset = Vector3.down * (weight.GetComponent<BoxCollider>().size.y / 2);
            weight.transform.position = _weightSlots[_weightsOnScale.Count].position;
            weight.transform.position -= offset;

            _weightsOnScale.Add(weight);
            _totalWeight += weight.WeightValue;
            OnWeightChanged?.Invoke(_totalWeight);
        }

        private void OnTriggerExit(Collider other)
        {
         
            Weight weight = other.GetComponent<Weight>();
            if (weight == null) return;
            
            _weightsOnScale.Remove(weight);
            _totalWeight -= weight.WeightValue;
            OnWeightChanged?.Invoke(_totalWeight);
            
        }
    }
}

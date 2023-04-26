using UnityEngine;

namespace Interactables
{
    public class Weight : PickupObject
    {
        private string _weightName;
        [SerializeField] private int weightValue;
        public int WeightValue => weightValue;
    }
}

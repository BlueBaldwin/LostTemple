using UnityEngine;

namespace Interactables
{
    public class PickupObject : Interactable
    {
        private bool _isPickedUp;
        public bool IsPickedUp => _isPickedUp;

        public override void OnInteract(PlayerController playerController)
        {
            if (!_isPickedUp && playerController.HeldObject == null)
            {
                Pickup(playerController);
            }
            else if (_isPickedUp && playerController.HeldObject == this)
            {
                Drop(playerController);
            }
        }

        private void Pickup(PlayerController playerController)
        {
            Transform transform1;
            (transform1 = transform).SetParent(playerController.HoldPosition);
            transform1.localPosition = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            _isPickedUp = true;
            playerController.HeldObject = this;
        }

        private void Drop(PlayerController playerController)
        {
            transform.SetParent(null);
            GetComponent<Rigidbody>().isKinematic = false;
            _isPickedUp = false;
            playerController.HeldObject = null;
        }
    }
}
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
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero; // Ensure that no forces are acting on the object.
            rb.angularVelocity = Vector3.zero;

            transform.SetParent(playerController.HoldPosition);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity; // Reset rotation

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
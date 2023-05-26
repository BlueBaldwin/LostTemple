using System.Collections;
using UnityEngine;

namespace Puzzles
{
    // Handling the weight puzzle door opening movement logic
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private GameObject rightDoor;
        [SerializeField] private float doorOpenSpeed;

        public void Open()
        {
            StartCoroutine(OpenCoroutine());
        }

        private IEnumerator OpenCoroutine()
        {
            CameraShake.Instance.ShakeCamera();
            Vector3 leftDoorStartPosition = leftDoor.transform.position;
            Vector3 rightDoorStartPosition = rightDoor.transform.position;
            float slideAmount = 1.5f;

            Vector3 leftDoorEndPosition = leftDoorStartPosition + slideAmount * Vector3.left;
            Vector3 rightDoorEndPosition = rightDoorStartPosition + slideAmount * Vector3.right;
            float time = 0;
            while (time < 1)
            {
                leftDoor.transform.position = Vector3.Lerp(leftDoorStartPosition, leftDoorEndPosition, time);
                rightDoor.transform.position = Vector3.Lerp(rightDoorStartPosition, rightDoorEndPosition, time);

                yield return null;
                time += Time.deltaTime * doorOpenSpeed;
            }
        }
    }
}
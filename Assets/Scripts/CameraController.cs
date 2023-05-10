using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    { 
        public Transform target;
        public float distance = 10.0f;
        public float height = 5.0f;

        [Header("Screen Shake")]
        public float shakeDuration = 0.5f;
        public AnimationCurve shakeCurve;

        private bool _isCameraShaking = false;
        private Vector3 _originalPosition;
        private float _shakeStrength;

        void LateUpdate()
        { if (_isCameraShaking)
            {
                Vector3 shakeVector = Random.insideUnitSphere * _shakeStrength;
                Vector3 shakeOffset = new Vector3(shakeVector.x, shakeVector.y, 0);
                transform.position += shakeOffset;
            }
            // Switched to cinemashine insrted of this
            
            // if (!_isCameraShaking)
            // {
            //     var cameraTransform = transform;
            //     var position = target.position;
            //     var position1 = cameraTransform.position;
            //     Vector3 targetPosition = new Vector3(position.x, position.y + height, position.z - distance);
            //     position1 = targetPosition;
            //     cameraTransform.position = position1;
            //     transform.rotation = Quaternion.LookRotation(position - position1);
            // }
            // else
            // {
            //     Vector3 shakeVector = Random.insideUnitSphere * _shakeStrength;
            //     Vector3 shakeOffset = new Vector3(shakeVector.x, shakeVector.y, 0);
            //     transform.position += shakeOffset;
            // }
        }

        public void StartScreenShake()
        {
            if (!_isCameraShaking)
            {
                StartCoroutine(ScreenShakeCoroutine());
            }
        }

        private IEnumerator ScreenShakeCoroutine()
        {
            _isCameraShaking = true;
            float elapsedTime = 0f;
            _originalPosition = transform.position;

            while (elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;

                _shakeStrength = shakeCurve.Evaluate(elapsedTime / shakeDuration);
                Vector3 shakeVector = Random.insideUnitSphere * _shakeStrength;

                transform.position = _originalPosition + shakeVector;

                yield return null;
            }

            transform.position = _originalPosition;
            _isCameraShaking = false;
        }
    }
}
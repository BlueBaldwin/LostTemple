using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Puzzles
{
    public class PuzzleDoor : MonoBehaviour
    {
        // inspector header
        [Header("Door Settings")]
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private GameObject rightDoor;
        [SerializeField] private float doorOpenSpeed;
        
        [Header("Scale Settings")]
        [SerializeField] private Scale scale1;
        [SerializeField] private Scale scale2;
        [SerializeField] private int answerWeight;

        [Header("Camera Shake Settings")]
        [SerializeField] private float shakeDuration;
        
        private int _scale1Weight;
        private int _scale2Weight;
        private bool _isDoorOpen;

        private CameraController _cameraController;
        
        private void OnEnable()
        {
            scale1.OnWeightChanged += UpdateScale1Weight;
            scale2.OnWeightChanged += UpdateScale2Weight;
        }

        private void OnDisable()
        {
            scale1.OnWeightChanged -= UpdateScale1Weight;
            scale2.OnWeightChanged -= UpdateScale2Weight;
        }

        private void Awake()
        {
            _cameraController = FindObjectOfType<CameraController>();
        }

        private void UpdateScale1Weight(int weightChange)
        {
            Debug.Log($"Scale 1 Weight Change: {weightChange}");
            _scale1Weight = weightChange;
            CheckPuzzleSolved();
        }

        private void UpdateScale2Weight(int weightChange)
        {
            Debug.Log($"Scale 2 Weight Change: {weightChange}");
            _scale2Weight = weightChange;
            CheckPuzzleSolved();
        }
        
        private void CheckPuzzleSolved()
        { 
            if (_scale1Weight + _scale2Weight == answerWeight)
            {
                Debug.Log("Correct Weight! Door is opening!");
                if (!_isDoorOpen)
                {
                    StartCoroutine(OpenDoor());
                    _isDoorOpen = true;
                }
            }
        }

        public bool go;
        private void Update()
        {
            if (go)
            {
                go = false;
                Debug.Log("Correct Weight! Door is opening!");
                StartCoroutine(OpenDoor());

            }
        }

        private IEnumerator OpenDoor()
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
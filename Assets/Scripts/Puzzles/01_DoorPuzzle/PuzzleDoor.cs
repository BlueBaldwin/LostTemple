using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using DefaultNamespace;
using RewardSystem;
using UnityEngine;

namespace Puzzles
{
    // This class is the logic for the scales puzzle and checks the weight of the scales to see if the puzzle is solved
    public class PuzzleDoor : MonoBehaviour
    {
        [Header("Door Settings")] 
        [SerializeField] private Door door;
        [SerializeField] private Scale scale1;
        [SerializeField] private Scale scale2;
        [SerializeField] private int answerWeight;

        [Header("Camera Shake Settings")] [SerializeField]
        private float shakeDuration;

        private int _scale1Weight;
        private int _scale2Weight;
        private bool _isDoorOpen;

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
                    StartCoroutine(PlayDialogAndOpenDoor());
                    PuzzleManager.PuzzleSolved("WeightPuzzle", 5);
                    _isDoorOpen = true;
                }
            }
        }

        private IEnumerator PlayDialogAndOpenDoor()
        {
            DialogManager.Instance.PlayEventDialog(1);
            yield return new WaitForSeconds(DialogManager.Instance.GetDialogClipLength(1));
            door.Open();
            DialogManager.Instance.PlayEventDialog(2);
        }
    }
}
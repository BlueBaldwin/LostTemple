using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzles
{
    public class ToggleBoardPuzzle : MonoBehaviour
    {
        [SerializeField] private Camera puzzleCamera;
        [SerializeField] private GameObject canvas;
        private PuzzleTrigger _puzzleTrigger;
        private bool _boardPuzzleActive;

        private void Awake()
        {
            _puzzleTrigger = GetComponentInChildren<PuzzleTrigger>();
            puzzleCamera.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _puzzleTrigger.onPuzzleBoardTriggered += TogglePuzzle;
            Debug.Log("Puzzle Triggered");
        }
        
        private void OnDisable()
        {
            _puzzleTrigger.onPuzzleBoardTriggered -= TogglePuzzle;
        }

        private void TogglePuzzle()
        {
            _boardPuzzleActive = true;
            puzzleCamera.gameObject.SetActive(true);
            canvas.gameObject.SetActive(true);
        }
    }
}
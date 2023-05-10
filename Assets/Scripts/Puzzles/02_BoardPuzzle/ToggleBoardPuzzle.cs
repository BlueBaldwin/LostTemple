using System;
using UnityEngine;
using Cinemachine;

namespace Puzzles
{
    public class ToggleBoardPuzzle : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        private PuzzleTrigger _puzzleTrigger;
        private bool _boardPuzzleActive;

        [SerializeField] private CinemachineVirtualCamera puzzleVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera playerVirtualCamera;
        private void Awake()
        {
            _puzzleTrigger = GetComponentInChildren<PuzzleTrigger>();
        }

        private void OnEnable()
        {
            _puzzleTrigger.onPuzzleBoardTriggered += TogglePuzzle;

        }
        
        private void OnDisable()
        {
            _puzzleTrigger.onPuzzleBoardTriggered -= TogglePuzzle;
        }

        private void TogglePuzzle()
        {
            Debug.Log("Puzzle Triggered");
            _boardPuzzleActive = true;
            if (_boardPuzzleActive)
            {
                puzzleVirtualCamera.Priority = 2;
                canvas.gameObject.SetActive(true);
            }
            else if (!_boardPuzzleActive)
            {
                puzzleVirtualCamera.Priority = 0;
                canvas.gameObject.SetActive(false);
            }
            
        }
    }
}
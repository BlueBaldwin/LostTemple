using UnityEngine;
using Cinemachine;

namespace Puzzles
{
    public class ToggleBoardPuzzle : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        private PuzzleTrigger _puzzleTrigger;
        private bool _boardPuzzleActive;
        
        [SerializeField] private CinemachineVirtualCamera playerVirtualCamera;
        [SerializeField] private Camera puzzleInputCamera;
        [SerializeField] private CinemachineVirtualCamera puzzleVirtualCamera;
        
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
            _boardPuzzleActive = !_boardPuzzleActive;
            
            if (_boardPuzzleActive)
            {
                puzzleVirtualCamera.Priority = 2;
                canvas.gameObject.SetActive(true);
            }
            else
            {
                puzzleVirtualCamera.Priority = 0;
                canvas.gameObject.SetActive(false);
            }
        }

        
    }
}

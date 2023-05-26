using UnityEngine;
using Cinemachine;
using RewardSystem;

namespace Puzzles
{
    // A simple class that toggles the puzzle camera on and off, and disables the puzzle trigger after the puzzle is solved
    public class ToggleBoardPuzzle : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private CinemachineVirtualCamera playerVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera puzzleVirtualCamera;
        
        private PuzzleTrigger _puzzleTrigger;
        
        private void Awake()
        {
            _puzzleTrigger = GetComponentInChildren<PuzzleTrigger>();
        }

        private void OnEnable()
        {
            _puzzleTrigger.onPuzzleBoardToggle += TogglePuzzleCameraOn;
            PuzzleManager.OnPuzzleSolved += TogglePuzzleCameraOff;
        }

        private void OnDisable()
        {
            _puzzleTrigger.onPuzzleBoardToggle -= TogglePuzzleCameraOn;
            PuzzleManager.OnPuzzleSolved -= TogglePuzzleCameraOff;
        }

        private void TogglePuzzleCameraOn()
        {
            Debug.Log("Puzzle Triggered");
            
            puzzleVirtualCamera.Priority = 1;
            playerVirtualCamera.Priority = 0;
            canvas.gameObject.SetActive(true);
        }
        
        private void TogglePuzzleCameraOff(PuzzleSolvedEvent puzzleSolvedEvent)
        {
            if (puzzleSolvedEvent.PuzzleId == "NumberMatchPuzzle")
            {
                puzzleVirtualCamera.Priority = 0;
                playerVirtualCamera.Priority = 1;
                canvas.gameObject.SetActive(false);
                
                if (_puzzleTrigger != null)
                {
                    Destroy(_puzzleTrigger);
                }
            }
            
        }

        
    }
}

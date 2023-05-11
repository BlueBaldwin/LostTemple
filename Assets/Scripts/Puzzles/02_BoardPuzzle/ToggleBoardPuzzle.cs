using UnityEngine;
using Cinemachine;

namespace Puzzles
{
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
            NumberMatchManager.OnPuzzleComplete += TogglePuzzleCameraOff;
        }

        private void OnDisable()
        {
            _puzzleTrigger.onPuzzleBoardToggle -= TogglePuzzleCameraOn;
            NumberMatchManager.OnPuzzleComplete -= TogglePuzzleCameraOff;
        }

        private void TogglePuzzleCameraOn()
        {
            Debug.Log("Puzzle Triggered");
            
            puzzleVirtualCamera.Priority = 1;
            playerVirtualCamera.Priority = 0;
            canvas.gameObject.SetActive(true);
        }
        
        private void TogglePuzzleCameraOff()
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

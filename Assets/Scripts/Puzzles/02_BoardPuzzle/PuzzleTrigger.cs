using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public Action onPuzzleBoardToggle;
    public static bool IsPuzzleBoardActive { get; set; }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPuzzleBoardToggle?.Invoke();
            PlayerController.CanMove = false;
            IsPuzzleBoardActive = true;
        }
    }
}

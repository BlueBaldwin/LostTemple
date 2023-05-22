using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public Action onPuzzleBoardToggle;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPuzzleBoardToggle?.Invoke();
            PlayerController.CanMove = false;
        }
    }
}

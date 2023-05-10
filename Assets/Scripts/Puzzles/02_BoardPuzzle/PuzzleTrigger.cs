using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public Action onPuzzleBoardTriggered;

    private bool _isPuzzleComplete;

    private void OnEnable()
    {
       NumberMatchManager.OnPuzzleComplete += SetCompletionState;
    }
    
    private void OnDisable()
    {
        NumberMatchManager.OnPuzzleComplete -= SetCompletionState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isPuzzleComplete)
        {
            onPuzzleBoardTriggered?.Invoke();    
        }
    }

    private void SetCompletionState()
    {
        _isPuzzleComplete = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoardCamera : MonoBehaviour
{
    private PuzzleTrigger _puzzleTrigger;
    private Animator _animator;
    private bool _isPuzzleCamera;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _puzzleTrigger = FindObjectOfType<PuzzleTrigger>();
    }

    private void OnEnable()
    {
        _puzzleTrigger.onPuzzleBoardTriggered += SwitchCamera;
    }

    private void OnDisable()
    {
       _puzzleTrigger.onPuzzleBoardTriggered -= SwitchCamera;
    }
    
    private void SwitchCamera()
    {
        if (!_isPuzzleCamera)
        {
            _animator.Play("PuzzleBoardCamera");
        }
        else
        {
            _animator.Play("PlayerCamera");
        }
    }
}

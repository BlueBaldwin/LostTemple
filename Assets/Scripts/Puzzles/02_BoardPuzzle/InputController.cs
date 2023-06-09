﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;
using RewardSystem;

namespace Puzzles
{
    // Handles the input for the number match puzzle, using a specific camera for raycasting into the in world puzzle board tiles
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Camera puzzleInputCamera;
        
        private NumberMatchManager _numberMatchManager;
        private Tile _highlightedTile;
        private Tile _selectedTile1;
        private Tile _selectedTile2;
        private bool _isPuzzleCompleted;

        private void Awake()
        {
            _numberMatchManager = GetComponent<NumberMatchManager>();
        }

        private void OnEnable()
        {
            PuzzleManager.OnPuzzleSolved += DisableInput;
        }

        private void OnDisable()
        {
            PuzzleManager.OnPuzzleSolved  -= DisableInput;
        }

        private void Update()
        {
            if (!_isPuzzleCompleted && PuzzleTrigger.IsPuzzleBoardActive)
            {
                HandleMouseInput();
            }
        }
        
        private void HandleMouseInput()
        {
            Tile selectedTile;
            Tile newHighlightedTile;
            GetMouseInput(puzzleInputCamera, out selectedTile, out newHighlightedTile);

            if (selectedTile != null)
            {
                string name = selectedTile.name;
                // Debug.Log($"Tile {name} clicked, number: {selectedTile.GetNumber()}");

                if (_selectedTile1 == null)
                {
                    _selectedTile1 = selectedTile;
                    _selectedTile1.SetHighlightedColor(true);
                }
                else if (_selectedTile2 == null && _selectedTile1 != selectedTile)
                {
                    _selectedTile2 = selectedTile;
                    _selectedTile2.SetHighlightedColor(true);

                    // Compare the selected tiles
                    if (_selectedTile1.GetNumber() + _selectedTile2.GetNumber() == PuzzleGenerator.Instance.Solutions[_numberMatchManager.RoundIndex].answer)
                    {
                        Debug.Log("Correct answer!");

                        // Move to the next round
                        StartCoroutine(NewRound());
                        StartCoroutine(ResetTileSelection());
                    }
                    else
                    {
                        Debug.Log("Incorrect answer!");

                        // Clear the selection
                        _selectedTile1.ShowIncorrectChoice();
                        _selectedTile2.ShowIncorrectChoice();
                        StartCoroutine(ResetTileSelection());
                    }
                }
            }
        }

        private IEnumerator ResetTileSelection()
        {
            // create a delay coroutine for 1 second
            yield return new WaitForSeconds(1f);

            if (_selectedTile1 != null)
            {
                _selectedTile1.SetHighlightedColor(false);
                _selectedTile1 = null;
            }

            if (_selectedTile2 != null)
            {
                _selectedTile2.SetHighlightedColor(false);
                _selectedTile2 = null;
            }
        }

        private IEnumerator NewRound()
        {
            yield return new WaitForSeconds(1f);
            _numberMatchManager.UpdateRoundIndex();
        }

        private void GetMouseInput(Camera puzzleCamera, out Tile selectedTile, out Tile highlightedTile)
        {
            selectedTile = null;
            highlightedTile = null;

            // Check if the left mouse button is pressed
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = puzzleCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);
                if (hit)
                {
                    selectedTile = hit.collider.GetComponent<Tile>();
                }
            }

            // Check if the mouse is hovering over a tile
            Ray hoverRay = puzzleCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit2D hoverHit = Physics2D.Raycast(hoverRay.origin, hoverRay.direction, 100);
            if (hoverHit)
            {
                highlightedTile = hoverHit.collider.GetComponent<Tile>();
            }

            // Create a debug ray to see where the mouse is pointing
            Debug.DrawRay(hoverRay.origin, hoverRay.direction * 100, Color.red);
        }

        private void DisableInput(PuzzleSolvedEvent puzzleSolvedEvent)
        {
            if (puzzleSolvedEvent.PuzzleId == "NumberMatchPuzzle")
            {
                _isPuzzleCompleted = true;
            }
        }
    }
}

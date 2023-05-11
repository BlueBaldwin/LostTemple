using System;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Puzzles
{
    public class NumberMatchManager : MonoBehaviour
    {
        [Header("Puzzle Settings")]
        [SerializeField] private TextMeshProUGUI _questionText;

        [SerializeField] private int _width, _height;
        [Header("Setup")]
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private GameObject _gridPosition;
        [SerializeField] private Camera puzzleInputCamera;
        
        private List<Tile> _tiles;
        private Tile _highlightedTile;
        private int _roundIndex;
        private List<AnswerSolution> _puzzleSolutions;
        public int RoundIndex => _roundIndex;

        public event Action OnPuzzleGenerate;
        
        public static event Action<int> OnPuzzleComplete;

        private void Awake()
        {
            _tiles = new List<Tile>();
            _puzzleSolutions = new List<AnswerSolution>();
            _roundIndex = 0;
        }

        void Start()
        {
            PopulatePuzzleLists();
            ConstructGrid();
            AssignNumbersToTiles();
        }

        void ConstructGrid()
        {
            _tiles.Clear();
            _tiles = GridConstruction.GenerateGridAndMoveToPosition(_width, _height, _tilePrefab, _gridPosition.transform);
            AssignAnswers();
            // Manually setting the cameras transform to ensure the puzzle is centred
            var position = _gridPosition.transform.position;
            var transform1 = puzzleInputCamera.transform;
            transform1.position = new Vector3(position.x, position.y, transform1.position.z);
        }
        
        private void AssignNumbersToTiles()
        {
            FillerNumberGenerator.AssignNumbersToTiles(_tiles, _puzzleSolutions[_roundIndex]);
        }

        void UpdateQuestionText(int targetNumber)
        {
            _questionText.text = $"Pick two numbers that make: {targetNumber}";
        }

        private void PopulatePuzzleLists()
        {
            _puzzleSolutions.Clear();
            _puzzleSolutions = PuzzleGenerator.Instance.Solutions;
            _roundIndex = 0;
            var targetSolution = _puzzleSolutions[_roundIndex];
            UpdateQuestionText(targetSolution.answer);
            
        }

        private void AssignAnswers()
        {
            // Add solution part 1
            int index = Random.Range(0, _tiles.Count);
            Tile randomTile1 = _tiles[index];
            int solutionIndex = Random.Range(0, PuzzleGenerator.Instance.Solutions[_roundIndex].solutionPairs.Count);

            int solutionNumber1 = PuzzleGenerator.Instance.Solutions[_roundIndex].solutionPairs[solutionIndex]
                .firstNumber;
            randomTile1.SetNumber(solutionNumber1);
            randomTile1.HasSolutionNumber = true;
            // Ensure the solution number is not used again
            FillerNumberGenerator.UsedNumbers.Add(solutionNumber1);

            // Add solution part 2
            int index2 = Random.Range(0, _tiles.Count);
            while (index2 == index)
            {
                index2 = Random.Range(0, _tiles.Count);
            }
            Tile randomTile2 = _tiles[index2];

            int solutionNumber2 = PuzzleGenerator.Instance.Solutions[_roundIndex].solutionPairs[solutionIndex]
                .secondNumber;
            randomTile2.SetNumber(solutionNumber2);
            randomTile2.HasSolutionNumber = true;
            // Ensure the solution number is not used again
            FillerNumberGenerator.UsedNumbers.Add(solutionNumber2);
            // Debug.Log("The Answers are: " + solutionNumber1 + " and " + solutionNumber2);
        }
        
        public void UpdateRoundIndex()
        {
            _roundIndex++;
            if (_roundIndex == PuzzleGenerator.Instance.PuzzleRounds)
            {
                Debug.Log("Puzzle Complete");
                OnPuzzleComplete?.Invoke(GetPlayersScore());
            }
            else
            {
                ResetPuzzle();
            }
            
        }

        private void ResetPuzzle()
        {
            // Clear out the text of the tiles
            foreach (var tile in _tiles)
            {
                tile.SetNumber(0);
                tile.HasSolutionNumber = false;
            }

            // Assign new answers and other numbers to the tiles
            FillerNumberGenerator.UsedNumbers.Clear();
            AssignAnswers();
            AssignNumbersToTiles();

            // Update the question text
            UpdateQuestionText(_puzzleSolutions[_roundIndex].answer);
        }

        private int GetPlayersScore()
        {
            int score = 5;
            // TO:DO Calculate the players score
    
            return score;
        }
    }
}
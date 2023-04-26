using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Puzzles
{
    public class PuzzleGenerator : MonoBehaviour
    {
        public static PuzzleGenerator Instance { get; private set; }
        
        // Puzzle difficulty = the number of rounds to complete and max number of addition calculation
        [SerializeField] private PuzzleDifficulty puzzleDifficulty;
        [SerializeField] private int puzzleRounds;
        // FOR DEBUGGING TO SEE THE ANSWERS
        [SerializeField] private List<int> answers;
        [SerializeField] private List<AnswerSolution> solutions;
        // Properties for the answers and solutions
        public List<int> Answers => answers;
        public List<AnswerSolution> Solutions => solutions;
        public int MaxNumber => (int)puzzleDifficulty;
        public int PuzzleRounds => puzzleRounds;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            GeneratePuzzle();
        }

        private void GeneratePuzzle()
        {
            answers.Clear();
            solutions.Clear();

            GenerateAnswers();
            GenerateSolutions();
        }

        private void GenerateAnswers()
        {
            // Creates the answers and adds it only if the answer is already in the list
            for (int i = 0; i < puzzleRounds; i++)
            {
                (int min, int max) = GetMinMaxForDifficulty(puzzleDifficulty);
                int tempAnswer = Random.Range(min, max + 1);
                while (answers.Contains(tempAnswer))
                {
                    tempAnswer = Random.Range(min, max + 1);
                }

                answers.Add(tempAnswer);
            }
        }

        // Returns a tuple - returning two ints for the min and max :)
        private (int min, int max) GetMinMaxForDifficulty(PuzzleDifficulty difficulty)
        {
            int max = (int)difficulty;
            int min = max / 2;

            return (min, max);
        }

        private void GenerateSolutions()
        {
            foreach (int answer in answers)
            {
                List<NumberPair> answerSolutions = new List<NumberPair>();

                for (int i = 2; i < answer; i++) // Start from 2 instead of 1
                {
                    answerSolutions.Add(new NumberPair(i, answer - i));
                }

                solutions.Add(new AnswerSolution { answer = answer, solutionPairs = answerSolutions });
            }
        }

    }
}
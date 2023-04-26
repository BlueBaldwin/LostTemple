using System.Collections.Generic;
using UnityEngine;

namespace Puzzles
{
    public class FillerNumberGenerator : MonoBehaviour
    {
        public static List<int> UsedNumbers { get; private set; } = new List<int>();

        public static int TileNumberGenerator(int currentAnswer, List<int> usedNumbers, int maxNumber)
        {
            // Create a list of potential numbers
            List<int> potentialNumbers = new List<int>();
            for (int i = 2; i <= maxNumber; i++)
            {
                if (i != currentAnswer && currentAnswer - i != 1 && !usedNumbers.Contains(i))
                {
                    potentialNumbers.Add(i);
                }
            }

            // If there are no potential numbers left, increase the max number range and try again
            if (potentialNumbers.Count == 0)
            {
                return TileNumberGenerator(currentAnswer, usedNumbers, (int)(maxNumber * 1.5f));
            }

            // Choose a random number from the potential numbers and remove it from the list
            int index = Random.Range(0, potentialNumbers.Count);
            int randomNumber = potentialNumbers[index];
            potentialNumbers.RemoveAt(index);

            usedNumbers.Add(randomNumber);

            return randomNumber;
        }



        public static void AssignNumbersToTiles(List<Tile> tiles, AnswerSolution currentSolution)
        {
            foreach (var t in tiles)
            {
                if (!t.HasSolutionNumber)
                {
                    t.SetNumber(TileNumberGenerator(currentSolution.answer, FillerNumberGenerator.UsedNumbers, PuzzleGenerator.Instance.MaxNumber));
                }
            }
        }

    }
}
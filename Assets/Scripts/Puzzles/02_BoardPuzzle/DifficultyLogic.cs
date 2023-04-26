using System;
using UnityEngine;

namespace Puzzles
{
    public class DifficultyLogic : MonoBehaviour
    {
        public event Action<PuzzleDifficulty> OnChangeDifficulty;
        
        public void ChangeDifficulty(int difficulty)
        {
            PuzzleDifficulty puzzleDifficulty = (PuzzleDifficulty) difficulty;
            OnChangeDifficulty?.Invoke(puzzleDifficulty);
        }
    }
}
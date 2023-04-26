using System.Collections.Generic;
using UnityEngine;

namespace Puzzles
{
    [System.Serializable]
    public class AnswerSolution
    {
        public int answer;
        public List<NumberPair> solutionPairs;
    }
        
    [System.Serializable]
    public class NumberPair
    {
        public int firstNumber;
        public int secondNumber;

        public NumberPair(int firstNumber, int secondNumber)
        {
            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
        }
    }
    
    
}
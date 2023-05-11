using System;
using TMPro;
using UnityEngine;

namespace RewardSystem
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        private int _totalScore;

        private void OnEnable()
        {
            Reward.onCollected += UpdateRewardScore;
        }

        private void UpdateRewardScore()
        {
            _totalScore++;
            scoreText.text = "Score: " + _totalScore;
        }
    }
}
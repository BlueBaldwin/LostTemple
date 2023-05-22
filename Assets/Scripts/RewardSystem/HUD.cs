using System;
using TMPro;
using DG.Tweening;
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
            IncreaseScore();
        }

        private void IncreaseScore()
        {
            scoreText.transform.DOScale(Vector3.one * 1.5f, 0.2f) 
                .SetEase(Ease.OutQuad) 
                .OnComplete(() => scoreText.transform.DOScale(Vector3.one, 0.2f)); 
        }
    }
}
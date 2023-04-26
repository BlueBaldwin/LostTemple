using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Puzzles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Color _baseColor, _offsetColor;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private TextMeshPro numberText;
        
        public bool HasSolutionNumber { get; set; } = false;

        private Color _initialColor;

        public void Init(bool isOffset, int number)
        {
            _renderer.color = isOffset ? _offsetColor : _baseColor;
            _renderer.color = _initialColor;
            SetNumber(number);
        }

        public float GetWidth()
        {
            return _renderer.bounds.size.x;
        }

        public void SetHighlight(bool active)
        {
            _highlight.SetActive(active);
        }

        public void SetNumber(int number)
        {
            numberText.text = number.ToString();
            Debug.Log($"Tile {gameObject.name} number set to: {number}");
        }
        public int GetNumber()
        {
            return Convert.ToInt32(numberText.text);
        }
        
        [SerializeField] private Color _highlightedColor;

        public void SetHighlightedColor(bool active)
        {
            _renderer.color = active ? _highlightedColor : _initialColor;
        }
    }
}
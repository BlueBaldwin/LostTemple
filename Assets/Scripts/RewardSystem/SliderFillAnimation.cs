using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderFillAnimation : MonoBehaviour
{
   [SerializeField] private Slider _slider;
   [SerializeField] private float _fillSpeed;

   private Image _fillImage;

   private void Start()
   {
      _fillImage = _slider.fillRect.GetComponent<Image>();
   }
   
   public void AnimateFill(float targetFillAmount)
   {
      // Animate the fill amount
      DOTween.To(() => _slider.value, x => _slider.value = x, targetFillAmount, _fillSpeed)
         .OnUpdate(() =>
         {
            _fillImage.color = Color.Lerp(Color.red, Color.green, _slider.normalizedValue);
         })
         .SetEase(Ease.OutQuad);
   }
}

using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Matchmaking.PlayerLife
{
    public class PlayerHealthSlider : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider healthSliderBack;

        public void SetAbsoluteValues(float absoluteHp)
        {
            healthSlider.maxValue = absoluteHp;
            healthSliderBack.maxValue = absoluteHp;
            healthSlider.value = absoluteHp;
            healthSliderBack.value = absoluteHp;
        }
        
        public async void OnHealthChange(float value)
        {
            if(value < 0) value = 0;
            
            await ChangeSliderValue(value, healthSlider, healthSlider.maxValue/2);
            await ChangeSliderValue(value, healthSliderBack, healthSlider.maxValue);
        }

        private async UniTask ChangeSliderValue(float value, Slider slider, float time)
        {
            while (slider.value > value)
            {
                slider.value -= Time.deltaTime * time;
                await UniTask.WaitForFixedUpdate();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts
{
    public class PlayerOptionsMenu : MonoBehaviour
    {
        public PlayerScripts.PlayerOptions PlayerOptions;

        [Header("Sliders")]
        public Slider DownSlider;
        public Slider HipSlider;
        public Slider AimSlider;

        [Header("Text")]
        public Text DownText;
        public Text HipText;
        public Text AimText;

        void Start()
        {
            DownSlider.onValueChanged.AddListener(delegate { OnSliderChanged(DownSlider, DownText); });
            HipSlider.onValueChanged.AddListener(delegate { OnSliderChanged(HipSlider, HipText); });
            AimSlider.onValueChanged.AddListener(delegate { OnSliderChanged(AimSlider, AimText); });
        }

        private void OnEnable()
        {
            InitSlider(DownSlider, DownText, PlayerOptions.LoweredSens);
            InitSlider(HipSlider, HipText, PlayerOptions.HipSens);
            InitSlider(AimSlider, AimText, PlayerOptions.AimSens);
        }

        public void OnSliderChanged(Slider slider, Text associatedText)
        {
            associatedText.text = $"{slider.transform.parent.name} : {slider.value.ToString("F2")}";
        }

        private void InitSlider(Slider slider, Text associatedText, float InitValue)
        {
            slider.value = InitValue;
            OnSliderChanged(slider, associatedText);
        }
    }
}

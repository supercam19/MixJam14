using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {
    [SerializeField] private int soundType;
    private AudioClip uiTick;
    public void OnSliderChange(float input)
    {
        float value = GetComponent<UnityEngine.UI.Slider>().value;
        SoundManager.SetVolume(value, soundType);
        SoundManager.Play(GameObject.Find("Player"), "ui_tick");
        
    }

    void Start() {
        uiTick = Resources.Load<AudioClip>("Sounds/ui_tick");
    }
}

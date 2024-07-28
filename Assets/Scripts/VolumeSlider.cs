using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {
    [SerializeField] private int soundType;
    private AudioClip uiTick;
    public void OnSliderChange()
    {
        float value = GetComponent<UnityEngine.UI.Slider>().value;
        if (soundType == 0) {
            SoundManager.SetVolume(value, soundType);
            if (soundType != 0) value *= SoundManager.masterVolume;
            AudioSource.PlayClipAtPoint(uiTick, GameObject.Find("Player").transform.position, value);
        }
    }

    void Start() {
        uiTick = Resources.Load<AudioClip>("Sounds/ui_tick");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

    public Slider volumeSlider;
    public AudioSource volumeAudio;

    // Use this for initialization
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    // Update is called once per frame
    void Update()
    {
       // volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        volumeAudio.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }
}

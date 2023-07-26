using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour {
    /* * * Constant Variables * * */
    const string VFX_VOLUME = "vfxVolume";
    const string SOUND_VOLUME = "soundVolume";

    [Header("Component")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    public void SetMusicVolume() {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat(SOUND_VOLUME,Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume() {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat(VFX_VOLUME,Mathf.Log10(volume) * 20);
    }
}

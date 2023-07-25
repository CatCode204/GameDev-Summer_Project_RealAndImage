using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Audio {
    public string name;
    public AudioClip audio;
}

public class AudioManagement : MonoBehaviour {
    /* * * Instance Variables * * */
    public static AudioManagement instance;

    [Header("Source")]
    [SerializeField] private AudioSource _backgroundSoruce;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _backgroundMusic;
    [SerializeField] private Audio[] _sfxSound;

    [Header("Setting")]
    [SerializeField] private float _waitBackgroundAudioDuration;

    /* * * Processing Variables * * */
    private int backgroundMusicIndex;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        } else Destroy(gameObject);
    }

    private void Start() {
        if (_backgroundMusic.Length > 0) StartCoroutine(PlayBackgroundMusic());
    }

    public void PlaySFXSound(string name) {
        int soundIndex = FindSFXSound(name);
        if (soundIndex == -1) {
            Debug.Log("Sound not found");
            return;
        }
        _sfxSource.PlayOneShot(_sfxSound[soundIndex].audio);
    }

    private int FindSFXSound(string name) {
        int index = -1;
        for (int i=0;i < _sfxSound.Length;i++)
            if (_sfxSound[i].name == name) index = i;
        return index;
    }

    private IEnumerator PlayBackgroundMusic() {
        while (true) {
            _backgroundSoruce.clip = _backgroundMusic[backgroundMusicIndex];
            _backgroundSoruce.Play();
            yield return new WaitForSeconds(_backgroundMusic[backgroundMusicIndex].length + _waitBackgroundAudioDuration);
            backgroundMusicIndex = backgroundMusicIndex == _backgroundMusic.Length ? backgroundMusicIndex + 1 : 0;
        }
    }
}

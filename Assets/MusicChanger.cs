using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private AudioClip rockClip;
    [SerializeField] private AudioClip clubClip;
    [SerializeField] private AudioClip chiptuneClip;

    private AudioSource _audioSource;
    private SceneSwitcher _sceneSwitcher;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        _sceneSwitcher.OnSwitchSceneEvent += SceneSwitcherOnOnSwitchSceneEvent;
    }

    private void SceneSwitcherOnOnSwitchSceneEvent(SceneState obj)
    {
        _audioSource.Stop();
        switch (obj)
        {
            case SceneState.rock:
                _audioSource.clip = rockClip;
                break;
            case SceneState.club:
                _audioSource.clip = clubClip;
                break;
            case SceneState.chiptune:
                _audioSource.clip = chiptuneClip;
                break;
        }
        _audioSource.Play();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private AudioClip rockClip;
    [SerializeField] private AudioClip rockClip2;
    
    [SerializeField] private AudioClip clubClip;
    [SerializeField] private AudioClip clubClip2;
    
    [SerializeField] private AudioClip chiptuneClip;
    [SerializeField] private AudioClip chiptuneClip2;
    

    private AudioSource _audioSource;
    private SceneSwitcher _sceneSwitcher;
    
    private int numberOfScenes = 3;

    private int songNum = 0;
    
    private int numberOfSongsPerScene = 2;

    private int sceneChangeCount = 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        _sceneSwitcher.OnSwitchSceneEvent += SceneSwitcherOnOnSwitchSceneEvent;
    }

    private void SceneSwitcherOnOnSwitchSceneEvent(SceneState obj)
    {
        sceneChangeCount++;
        if (sceneChangeCount == numberOfScenes)
        {
            sceneChangeCount = 0;
            songNum++;
            if (songNum == numberOfSongsPerScene)
            {
                songNum = 0;
            }
        }
            
        if (_audioSource.clip != null)
        {
            _audioSource.Stop();
        }
        switch (obj)
        {
            case SceneState.rock:
                if (songNum == 0)
                {
                    _audioSource.clip = rockClip;
                }
                else
                {
                    _audioSource.clip = rockClip2;
                }
                break;
            case SceneState.club:
                if (songNum == 0)
                {
                    _audioSource.clip = clubClip;
                }
                else
                {
                    _audioSource.clip = clubClip2;
                }
                break;
            case SceneState.chiptune:
                if (songNum == 0)
                {
                    _audioSource.clip = chiptuneClip;
                }
                else
                {
                    _audioSource.clip = chiptuneClip2;
                }
                break;
        }

        if (_audioSource.clip != null)
        {
            _audioSource.Play();
        }
    }
}

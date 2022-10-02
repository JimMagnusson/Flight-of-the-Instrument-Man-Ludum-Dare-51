using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneState
{
    rock,
    club,
    chiptune
}
public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject rockScene;
    [SerializeField] private GameObject clubScene;

    [SerializeField] private float sceneTime = 10f;
    
    
    private SceneState currentSceneState;
    private float sceneTimer;
    private GameObject currentScene;
    
    public event Action<SceneState> OnSwitchSceneEvent;
    
    void Start()
    {
        sceneTimer = sceneTime;
        currentScene = rockScene;
    }
    
    

    void Update()
    {
        
        sceneTimer -= Time.deltaTime;
        if (sceneTimer < 0)
        {
            SwitchScene();
            sceneTimer = sceneTime;
        }
        
    }

    private void SwitchScene()
    {
        currentScene.SetActive(false);
        switch (currentSceneState)
        {
            case SceneState.rock:
                currentScene = clubScene;
                currentSceneState = SceneState.club;
                break;
            case SceneState.club:
                currentScene = rockScene;
                currentSceneState = SceneState.rock;
                break;
            case SceneState.chiptune:
                // TODO:
                //currentScene = 
                break;
        }
        if (OnSwitchSceneEvent != null)
        {
            OnSwitchSceneEvent(currentSceneState);
        }
        currentScene.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject chiptuneScene;
    
    [SerializeField] private float sceneTime = 10f;

    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private Camera extraCamera;
    
    [SerializeField] private Camera someCamera;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private CinemachineVirtualCamera vCamArcade;
    [SerializeField] private RawImage pixelImage;
    
    //[SerializeField] private RenderTexture pixelRenderTexture;
    
    private SceneState currentSceneState;
    private float sceneTimer;
    private GameObject currentScene;
    private GameController _gameController;
    private PlayerController playerController;
    
    public event Action<SceneState> OnSwitchSceneEvent;
    
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        sceneTimer = sceneTime;
        currentScene = rockScene;
        _gameController = FindObjectOfType<GameController>();
    }
    void Update()
    {
        if (_gameController.GameState == GameState.retry)
        {
            return;
        }
        sceneTimer -= Time.deltaTime;
        if (sceneTimer < 0)
        {
            SwitchScene();
            sceneTimer = sceneTime;
        }

        if (currentSceneState == SceneState.chiptune)
        {
            playerController.cam.transform.position = extraCamera.transform.position;
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
                currentScene = chiptuneScene;
                currentSceneState = SceneState.chiptune;
                mainCamera.gameObject.SetActive(false);
                extraCamera.gameObject.SetActive(true);
                someCamera.gameObject.SetActive(true);
                pixelImage.enabled = true;
                //vCam.gameObject.SetActive(false);
                //vCamArcade.gameObject.SetActive(true);
                //playerController.cam = extraCamera;
                
                break;
            case SceneState.chiptune:
                currentScene = rockScene;
                currentSceneState = SceneState.rock;
                mainCamera.gameObject.SetActive(true);
                extraCamera.gameObject.SetActive(false);
                someCamera.gameObject.SetActive(false);
                pixelImage.enabled = false;
                //playerController.cam = mainCamera;
                //vCam.gameObject.SetActive(true);
                //vCamArcade.gameObject.SetActive(false);
                break;
        }
        if (OnSwitchSceneEvent != null)
        {
            OnSwitchSceneEvent(currentSceneState);
        }
        currentScene.SetActive(true);
    }
}

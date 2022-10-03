using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image RetryImage;
    [SerializeField] private Image PauseImage;
    [SerializeField] private Image StartGameImage;
    
    [SerializeField] private TextMeshProUGUI totalTimeTimerTMP;
    
    [SerializeField] private TextMeshProUGUI totalTimeRetryTMP;
    
    [SerializeField] private TextMeshProUGUI bestTimeRetryTMP;
    
    [SerializeField] private TextMeshProUGUI roundTimerTMP;
    
    [SerializeField] private int roundTime = 10;

    private float roundTimer = 0;
    
    private float totalTimeTimer = 0;

    private GameController _gameController;

    private void Start()
    {
        roundTimer = roundTime;
        _gameController = FindObjectOfType<GameController>();
    }
    
    public void ShowRetryImage()
    {
        RetryImage.gameObject.SetActive(true);
        totalTimeRetryTMP.SetText("Your time: {0:0} m {1:0} s", (int) totalTimeTimer/60, totalTimeTimer % 60);
        
        //bestTimeRetryTMP.SetText("Your best time: {0:0} m {1:0} s", (int) totalTimeTimer/60, totalTimeTimer % 60);
    }

    public void ToggleStartGameImage(bool boolean)
    {
        StartGameImage.gameObject.SetActive(boolean);
    }

    public void TogglePauseImage(bool boolean)
    {
        PauseImage.gameObject.SetActive(boolean);
    }
    

    public bool RetryImageScreenIsActive()
    {
        return RetryImage.gameObject.activeSelf;
    }

    public bool PauseImageScreenIsActive()
    {
        return PauseImage.gameObject.activeSelf;
    }

    public bool StartGameImageIsActive()
    {
        return StartGameImage.gameObject.activeSelf;
    }

    private void Update()
    {
        if (_gameController.GameState == GameState.retry)
        {
            return;
        }
        
        totalTimeTimer += Time.deltaTime;
        roundTimer -= Time.deltaTime;
        if (roundTimer <= 0)
        {
            //roundTimer = 0;
            roundTimer = roundTime;
        }

        /*
        string extraZero = "";
        if(!(roundTimer >= 10f))
        {
            extraZero = "0";
        }
        */
        
        roundTimerTMP.SetText("{0:0}", roundTimer);
        
        totalTimeTimerTMP.SetText("{0:0} m {1:0} s", (int) totalTimeTimer/60, totalTimeTimer % 60);
    }
}

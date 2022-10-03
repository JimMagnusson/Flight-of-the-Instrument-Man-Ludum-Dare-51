using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    
    private void OnEnable() {
        playerControls.Enable();
    }
    
    private void OnDisable() {
        playerControls.Disable();
    }


    private void Update()
    {
        if (playerControls.Standard.Retry.triggered)
        {
            Debug.Log("hej");
            FindObjectOfType<LevelLoader>().LoadNextScene();
        }
        
    }
}

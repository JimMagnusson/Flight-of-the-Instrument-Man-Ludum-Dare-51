using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchPlayer : MonoBehaviour
{
    [SerializeField] private float sceneTime = 10f;
    [SerializeField] private float scratchTime = 9f;

    private float timer;
    private float sceneTimer;
    private AudioSource _audioSource;

    private bool scratchPlayed = false;
    
    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        timer = scratchTime;
        _audioSource = GetComponent<AudioSource>();
        sceneTimer = sceneTime;
        _gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.GameState == GameState.retry)
        {
            return;
        }
        
        timer -= Time.deltaTime;
        if (timer < 0 && !scratchPlayed)
        {
            scratchPlayed = true;
            _audioSource.Play();
        }

        sceneTimer -= Time.deltaTime;
        if (sceneTimer < 0)
        {
            sceneTimer = sceneTime;
            timer = scratchTime;
            scratchPlayed = false;
        }
    }
}

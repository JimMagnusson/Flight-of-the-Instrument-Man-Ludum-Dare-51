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
    // Start is called before the first frame update
    void Start()
    {
        timer = scratchTime;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            _audioSource.Play();
        }

        sceneTimer -= Time.deltaTime;
        if (sceneTimer < 0)
        {
            timer = scratchTime;
        }
    }
}

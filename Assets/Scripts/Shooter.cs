using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform bulletSpawnPoint;
    
    [SerializeField] private Transform bulletsParent;
    
    [SerializeField] private float cooldownTime;
    
    [SerializeField] private AudioClip bulletSound;
    
    [SerializeField] private bool playSound = false;

    [SerializeField] private float bulletVol = 0.15f;

    //[SerializeField] private GameObject bulletParticles;
    
    private PlayerControls playerControls;

    private bool buttonPressed = false;
    
    private float cooldownTimer;
    
    private GameController _gameController;
    
    private AudioSource audioSource;
    

    private void Awake() {
        playerControls = new PlayerControls();
        _gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnEnable() {
        playerControls.Enable();
    }
    
    private void OnDisable() {
        playerControls.Disable();
    }

    void Start()
    {
        cooldownTimer = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.GameState == GameState.retry)
        {
            return;
        }
        if (bulletPrefab == null)
        {
            return;
        }
        buttonPressed = playerControls.Standard.Attack.IsPressed();
        cooldownTimer -= Time.deltaTime;
        if (buttonPressed && cooldownTimer < 0)
        {
            cooldownTimer = cooldownTime;

            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation, bulletsParent).GetComponent<Bullet>();

            //Instantiate(bulletParticles, transform.position, transform.rotation);
            
            bullet.SetForwardVector(transform.forward);
            if (playSound)
            {
                audioSource.PlayOneShot(bulletSound, bulletVol);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform bulletSpawnPoint;
    
    [SerializeField] private Transform bulletsParent;
    
    [SerializeField] private float cooldownTime;
    
    private PlayerControls playerControls;

    private bool buttonPressed = false;
    
    private float cooldownTimer;

    private void Awake() {
        playerControls = new PlayerControls();
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
            bullet.SetForwardVector(transform.forward);
        }
    }
}

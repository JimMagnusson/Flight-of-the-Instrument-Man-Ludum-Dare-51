using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float damageOnCollision = 2f; // TODO: move out?

    [SerializeField] private AudioClip deathSFX;
    
    private Health health;
    private AudioSource audioSource;

    public float GetDamageOnCollision()
    {
        return damageOnCollision;
    }
    void Start()
    {
        health = GetComponent<Health>();
        //audioSource.GetComponent<AudioSource>();
        health.OnDeathEvent += HealthOnOnDeathEvent;
    }

    private void HealthOnOnDeathEvent(Health obj)
    {
        /*
        // TODO: Death VFX and SFX;
        if (deathSFX != null)
        {
            audioSource.PlayOneShot(deathSFX);
        }
        */
        
    }
}
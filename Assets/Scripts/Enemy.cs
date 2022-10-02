using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float damageOnCollision = 2f; // TODO: move out?

    [SerializeField] private AudioClip deathSFX;
    
    [SerializeField] private float damageMaterialDuration = 0.2f;
    
    private Health health;
    private AudioSource audioSource;

    [SerializeField] private Material damageMaterial;
    private Material standardMaterial;
    [SerializeField] private MeshRenderer[] _meshRenderers;

    public float GetDamageOnCollision()
    {
        return damageOnCollision;
    }
    void Start()
    {
        health = GetComponent<Health>();
        //audioSource.GetComponent<AudioSource>();
        health.OnDeathEvent += HealthOnOnDeathEvent;
        standardMaterial = _meshRenderers[0].material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health.RecieveDamage(other.GetComponent<Bullet>().damage);
            StartCoroutine(DamageCoroutine());
        }
    }

    private IEnumerator DamageCoroutine()
    {
        //_meshRenderer.material = damageMaterial;
        foreach (MeshRenderer renderer in _meshRenderers)
        {
            renderer.material = damageMaterial;
        }
        yield return new WaitForSeconds(damageMaterialDuration);
        foreach (MeshRenderer renderer in _meshRenderers)
        {
            renderer.material = standardMaterial;
        }
        //_meshRenderer.material = standardMaterial;
    }


    private void HealthOnOnDeathEvent(Health obj)
    {
        // TODO: Death VFX and SFX;
        if (deathSFX != null)
        {
            audioSource.PlayOneShot(deathSFX);
        }
        Destroy(gameObject);
    }
}
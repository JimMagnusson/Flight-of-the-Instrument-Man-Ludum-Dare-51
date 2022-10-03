using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float damageOnCollision = 2f; // TODO: move out?

    [SerializeField] private AudioClip deathSFX;
    
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ParticleSystem deathParticles2;
    
    
    [SerializeField] private float damageMaterialDuration = 0.2f;
    
    private Health health;
    private AudioSource audioSource;

    [SerializeField] private Material damageMaterial;
    private Material standardMaterial;
    
    
    [SerializeField] private GameObject[] bodies;
    
    [SerializeField] private MeshRenderer[] _rockMeshRenderers;
    [SerializeField] private MeshRenderer[] _clubMeshRenderers;
    [SerializeField] private MeshRenderer[] _chiptuneMeshRenderers;

    private MeshRenderer[] currentMeshRenderers;
    
    private SceneSwitcher _sceneSwitcher;
    
    public float GetDamageOnCollision()
    {
        return damageOnCollision;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        //audioSource.GetComponent<AudioSource>();
        health.OnDeathEvent += HealthOnOnDeathEvent;
        standardMaterial = _rockMeshRenderers[0].material;
        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        _sceneSwitcher.OnSwitchSceneEvent += SceneSwitcherOnOnSwitchSceneEvent;
        
        currentMeshRenderers = _rockMeshRenderers;
    }

    private void SceneSwitcherOnOnSwitchSceneEvent(SceneState state)
    {
        switch (state)
        {
            case SceneState.rock:
                currentMeshRenderers = _rockMeshRenderers;
                break;
            case SceneState.club:
                currentMeshRenderers = _clubMeshRenderers;
                break;
            case SceneState.chiptune:
                currentMeshRenderers = _chiptuneMeshRenderers;
                break;
        }
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
        foreach (MeshRenderer renderer in currentMeshRenderers)
        {
            renderer.material = damageMaterial;
        }
        yield return new WaitForSeconds(damageMaterialDuration);
        foreach (MeshRenderer renderer in currentMeshRenderers)
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

        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Instantiate(deathParticles2, transform.position, Quaternion.identity);

        foreach (GameObject body in bodies)
        {
            body.SetActive(false);
        }
        
        _sceneSwitcher.OnSwitchSceneEvent -= SceneSwitcherOnOnSwitchSceneEvent;
        Destroy(gameObject, 2f);
        
    }
}
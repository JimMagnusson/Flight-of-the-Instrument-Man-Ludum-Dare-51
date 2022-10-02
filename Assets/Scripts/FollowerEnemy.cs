using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveTowardTarget))]
[RequireComponent(typeof(RotateTowardTarget))]
public class FollowerEnemy : MonoBehaviour
{
    [SerializeField] private bool canRotateTowardTarget = true;
    [SerializeField] private bool canMoveTowardTarget = true;


    [SerializeField] private GameObject rockBody;
    [SerializeField] private GameObject clubBody;
    [SerializeField] private GameObject chiptuneBody;
    
    private Health _health;
    private RotateTowardTarget _rotateTowardTarget;
    private MoveTowardTarget _moveTowardTarget;
    private SceneSwitcher _sceneSwitcher;
    private GameObject _currentBody;
    
    void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeathEvent += HealthOnOnDeathEvent;
        _rotateTowardTarget = GetComponent<RotateTowardTarget>();
        _moveTowardTarget = GetComponent<MoveTowardTarget>();
        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        _sceneSwitcher.OnSwitchSceneEvent += SceneSwitcherOnOnSwitchSceneEvent;
        _currentBody = rockBody;
    }

    private void SceneSwitcherOnOnSwitchSceneEvent(SceneState state)
    {
        _currentBody.SetActive(false);
        switch (state)
        {
            case SceneState.rock:
                _currentBody = rockBody;
                break;
            case SceneState.club:
                _currentBody = clubBody;
                break;
            case SceneState.chiptune:
                _currentBody = chiptuneBody;
                break;
        }
        _currentBody.SetActive(true);
    }

    private void HealthOnOnDeathEvent(Health obj)
    {
        _rotateTowardTarget.SetRotationActive(false);
        _moveTowardTarget.SetMovingActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: temp. remove
        if (collision.gameObject.CompareTag("Player"))
        {
            _health.RecieveDamage(1000);
        }
    }
}
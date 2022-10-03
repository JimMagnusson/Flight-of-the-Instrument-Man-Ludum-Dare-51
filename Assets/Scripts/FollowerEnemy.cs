using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private NavMeshAgent _navMeshAgent;
    private GameController _gameController;

    private void Awake()
    {
        _rotateTowardTarget = GetComponent<RotateTowardTarget>();
        _moveTowardTarget = GetComponent<MoveTowardTarget>();
        _currentBody = rockBody;
        _gameController = FindObjectOfType<GameController>();
    }

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _health = GetComponent<Health>();
        _health.OnDeathEvent += HealthOnOnDeathEvent;

        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        _sceneSwitcher.OnSwitchSceneEvent += SceneSwitcherOnOnSwitchSceneEvent;
    }

    private void Update()
    {
        if (_gameController.GameState == GameState.retry)
        {
            _rotateTowardTarget.SetRotationActive(false);
            _moveTowardTarget.SetMovingActive(false);
            _navMeshAgent.enabled = false;
        }
    }

    public void SetBody(SceneState state)
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
    
    public void SetTarget(Transform targ)
    {
        _moveTowardTarget.SetTarget(targ);
        _rotateTowardTarget.SetTarget(targ);
    }

    private void SceneSwitcherOnOnSwitchSceneEvent(SceneState state)
    {
        SetBody(state);
    }

    private void HealthOnOnDeathEvent(Health obj)
    {
        _rotateTowardTarget.SetRotationActive(false);
        _moveTowardTarget.SetMovingActive(false);
    }

    private void OnDestroy()
    {
        _sceneSwitcher.OnSwitchSceneEvent -= SceneSwitcherOnOnSwitchSceneEvent;
    }
}
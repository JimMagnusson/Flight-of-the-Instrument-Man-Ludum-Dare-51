using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ChargeState
{
    normal,
    chargePreparing,
    charging
}
public class Charger : MonoBehaviour
{
    [SerializeField] private float chargeLength = 5f;
    [SerializeField] private float chargePreparationTime = 3f;
    [SerializeField] private float chargeCooldownTime = 3f;
    [SerializeField] private float chargeVelocity = 10f;

    [SerializeField] private AudioClip chargePrepAudio;
    [SerializeField] private ParticleSystem chargePrepParticles;
    
    [SerializeField] private AudioClip chargeAudio;
    [SerializeField] private ParticleSystem chargeParticles;

    [SerializeField] private Transform target;
    
    [SerializeField] private GameObject rockBody;
    [SerializeField] private GameObject clubBody;
    [SerializeField] private GameObject chiptuneBody;

    private SceneSwitcher _sceneSwitcher;
    private GameObject _currentBody;
    
    private float chargeCooldownTimer;
    private float chargePreparationTimer;
    
    private bool chargePreparing = false;
    private bool charging = false;
    private AudioSource _audioSource;

    private NavMeshAgent _navMeshAgent;
    private Vector3 chargeStartPosition;
    private Vector3 chargeDirection;
    private MoveTowardTarget _moveTowardTarget;
    private RotateTowardTarget _rotateTowardTarget;
    private GameController _gameController;
    private Health _health;

    private ChargeState _chargeState = ChargeState.normal;

    private void Awake()
    {
        _rotateTowardTarget = GetComponent<RotateTowardTarget>();
        _moveTowardTarget = GetComponent<MoveTowardTarget>();
        _currentBody = rockBody;
        _gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        _health = GetComponent<Health>();
        chargeCooldownTimer = chargeCooldownTime;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        _sceneSwitcher.OnSwitchSceneEvent += SceneSwitcherOnOnSwitchSceneEvent;
        _health.OnDeathEvent += HealthOnOnDeathEvent;
    }
    
    private void HealthOnOnDeathEvent(Health obj)
    {
        _rotateTowardTarget.SetRotationActive(false);
        _moveTowardTarget.SetMovingActive(false);
        _sceneSwitcher.OnSwitchSceneEvent -= SceneSwitcherOnOnSwitchSceneEvent;
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
        target = targ;
        _moveTowardTarget.SetTarget(targ);
        _rotateTowardTarget.SetTarget(targ);
    }

    private void SceneSwitcherOnOnSwitchSceneEvent(SceneState state)
    {
        SetBody(state);
    }

    private void Update()
    {
        if (target == null)
        {
            Debug.Log("No target");
            return;
        }
        
        if (_gameController.GameState == GameState.retry)
        {
            _rotateTowardTarget.SetRotationActive(false);
            _moveTowardTarget.SetMovingActive(false);
            _navMeshAgent.enabled = false;
            return;
        }
        
        switch (_chargeState)
        {
            case ChargeState.normal:
                chargeCooldownTimer -= Time.deltaTime;
                if(chargeCooldownTimer < 0 && Vector3.Distance(transform.position, target.position) < chargeLength)
                {
                    // Stop moving toward enemy
                    _navMeshAgent.enabled = false;
                    _moveTowardTarget.SetMovingActive(false);
            
                    if (chargePrepParticles != null)
                    {
                        chargePrepParticles.Play();
                    }

                    if (chargePrepAudio != null)
                    {
                        _audioSource.PlayOneShot(chargePrepAudio);
                    }
            
                    // Start charge preparing
                    chargePreparationTimer = chargePreparationTime;
                    _chargeState = ChargeState.chargePreparing;
                }
                break;
            case ChargeState.chargePreparing:
                chargePreparationTimer -= Time.deltaTime;
                if (chargePreparationTimer < 0)
                {
                    chargePreparing = false;
                    Charge();
                    _chargeState = ChargeState.charging;
                }
                break;
            case ChargeState.charging:
                // Exit condition
                if (Vector3.Distance(chargeStartPosition, transform.position) > chargeLength)
                {
                    ExitCharge();
                }
                transform.position += chargeDirection * Time.deltaTime * chargeVelocity;
                break;
        }
    }

    private void Charge()
    {
        chargeDirection = target.position - transform.position;
        chargeDirection.Normalize();
        
        chargeStartPosition = transform.position;

        if (chargeParticles != null)
        {
            chargeParticles.Play();
        }

        if (chargeAudio != null)
        {
            _audioSource.PlayOneShot(chargeAudio);
        }
    }

    private void ExitCharge()
    {
        _chargeState = ChargeState.normal;
        _moveTowardTarget.SetMovingActive(true);
        _navMeshAgent.enabled = true;
        chargeCooldownTimer = chargeCooldownTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_chargeState == ChargeState.charging)
        {
            ExitCharge();
        }
    }
}

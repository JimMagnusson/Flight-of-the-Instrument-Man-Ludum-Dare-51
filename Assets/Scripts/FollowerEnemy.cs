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

    private Health _health;
    private RotateTowardTarget _rotateTowardTarget;
    private MoveTowardTarget _moveTowardTarget;
    
    void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeathEvent += HealthOnOnDeathEvent;
        _rotateTowardTarget = GetComponent<RotateTowardTarget>();
        _moveTowardTarget = GetComponent<MoveTowardTarget>();
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
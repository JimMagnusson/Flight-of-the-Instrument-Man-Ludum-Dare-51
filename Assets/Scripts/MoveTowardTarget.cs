using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float stopDistanceToTarget = 1f;
    [SerializeField] private bool movingActive = true;
    [SerializeField] private bool wheels = true;
   

    private WheelRotator _wheelRotator;
    private NavMeshAgent agent;

    
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = target.position; 
        if(wheels) {
            _wheelRotator = GetComponent<WheelRotator>();
        }
    }

    public void SetMovingActive(bool active)
    {
        movingActive = active;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if(!movingActive) {
            return;
        }
        if(wheels) {
            _wheelRotator.RotateWheels(true, moveSpeed);
        }
        agent.destination = target.position; 
    }
}
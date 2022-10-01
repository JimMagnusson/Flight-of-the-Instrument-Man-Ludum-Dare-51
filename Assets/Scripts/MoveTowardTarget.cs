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

    //private CharacterController controller;
    private NavMeshAgent agent;

    
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = target.position; 
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
        agent.destination = target.position; 
        /*
        if(!movingActive || target == null) { return; }
        Vector3 towardTarget = target.position - transform.position;
        // Move toward target until certain distance to target is reached.
        if(towardTarget.magnitude > stopDistanceToTarget)
        {
            towardTarget.Normalize();
            if (charMove)
            {
                towardTarget.y = 0;
                controller.Move(Time.deltaTime * towardTarget  * moveSpeed);
            }
            else
            {
                transform.position += new Vector3(towardTarget.x * moveSpeed * Time.deltaTime, 0, towardTarget.z * moveSpeed * Time.deltaTime);
            }
            //controller.Move(Time.deltaTime * towardTarget  * moveSpeed);
        }
        */
        
    }
}
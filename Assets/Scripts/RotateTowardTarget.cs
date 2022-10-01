using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float stopDistanceToTarget = 1f;
    [SerializeField] private bool rotationActive = true;

    public void SetRotationActive(bool active)
    {
        rotationActive = active;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if(!rotationActive || target == null) { return; }
        Vector3 towardTarget = target.position - transform.position;
        if (towardTarget.magnitude > stopDistanceToTarget)
        {
            Quaternion targetRotation = Quaternion.LookRotation(towardTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,Time.deltaTime * rotationSpeed);
        }
    }
}
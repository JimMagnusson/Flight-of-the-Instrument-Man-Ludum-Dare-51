using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    [SerializeField] private GameObject wheels;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] Vector3 axis = Vector3.right;

    [SerializeField] private bool active = true;

    public void RotateWheels(bool forward, float power)
    {
        if (wheels && active)
        {
            if (forward)
            {
                wheels.transform.Rotate(axis, Time.deltaTime * rotationSpeed  * power, Space.Self);
            }
            else
            {
                wheels.transform.Rotate(axis, -Time.deltaTime * rotationSpeed * power, Space.Self);
            }
            
        }
    }
}

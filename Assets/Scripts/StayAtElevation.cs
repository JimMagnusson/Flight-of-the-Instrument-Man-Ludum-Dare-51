using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayAtElevation : MonoBehaviour
{
    private float startYVal;

    private float maxDiff = 0.1f;

    private void Start()
    {
        startYVal = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.y - startYVal) > maxDiff)
        {
            transform.position = new Vector3(transform.position.x, startYVal, transform.position.z);
        }
    }
}

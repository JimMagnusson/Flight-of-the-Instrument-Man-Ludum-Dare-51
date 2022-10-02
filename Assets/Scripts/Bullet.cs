using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public float damage = 1f;

    private Vector3 forwardDirection = Vector3.forward;

    public void SetForwardVector(Vector3 forward)
    {
        forwardDirection = forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * speed * forwardDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: Hit SFX
        Destroy(this.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float hp = 10;

    [SerializeField] private bool alive = true;

    public event Action<Health> OnDeathEvent;

    public void RecieveDamage(float amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        alive = false;
        if (OnDeathEvent != null)
        {
            OnDeathEvent(this);
        }
    }

    public bool IsAlive()
    {
        return alive;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
    [SerializeField] protected float _healthAmount;

    public abstract void TakeDamage(float amount);

    protected abstract void Die();
}

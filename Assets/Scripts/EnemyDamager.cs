using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 1f;
    private Transform _transform;

    private void Start()
    {
        _transform = this.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Damagable>())
        {
            collision.GetComponent<Damagable>().TakeDamage(_damageAmount);

            if (collision.GetComponent<PlayerMovement>())
            {
                Vector2 direction = collision.transform.position - _transform.position;
                collision.GetComponent<PlayerMovement>().Knockback(direction.normalized);
            }
        }
    }
}

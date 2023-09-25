using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _damage = 2f;
    [SerializeField] private AudioClip _explosionSound;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Damagable>())
        {
            collision.GetComponent<Damagable>().TakeDamage(_damage);

            if (collision.GetComponent<PlayerMovement>())
            {
                Vector2 direction = collision.transform.position - this.transform.position;
                collision.GetComponent<PlayerMovement>().Knockback(direction.normalized);
            }
        }
    }
}

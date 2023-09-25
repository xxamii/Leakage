using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Dependecies
    [SerializeField] private GameObject _particlesPrefab;
    [SerializeField] private AudioClip _splashSound;
    private Transform _transform;
    private Rigidbody2D _rigidBody;

    // Values
    private float _speed = 20f;
    private float _lifeTime = 0.6f;
    [SerializeField] private float _damage = 1f;

    private void Start()
    {
        _transform = this.transform;
        _rigidBody = GetComponent<Rigidbody2D>();

        _rigidBody.velocity = _transform.right * _speed;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Damagable>())
        {
            collision.GetComponent<Damagable>().TakeDamage(_damage);

            if (collision.GetComponent<EnemyMovement>())
            {
                Vector2 direction = collision.transform.position - _transform.position;
                collision.GetComponent<EnemyMovement>().Knockback(direction.normalized);
            }
        }

        Die();
    }

    private void Die()
    {
        Instantiate(_particlesPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_splashSound, _transform.position);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyHealth : EnemyHealth
{
    // Dependencies
    [SerializeField] private GameObject _explosionPrefab;
    private Transform _transform;

    private void Start()
    {
        _transform = this.transform;
        this._animator = GetComponent<Animator>();
        this._audio = GetComponent<EnemyAudio>();
    }

    protected override void Die()
    {
        Instantiate(_explosionPrefab, _transform.position, Quaternion.identity);
        base.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            Die();
        }
    }
}

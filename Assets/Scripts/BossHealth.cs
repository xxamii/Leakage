using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    private Boss _boss;
    private float _maxHealth;
    [SerializeField] private PolygonCollider2D _collider;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boss = GetComponent<Boss>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _maxHealth = this._healthAmount;
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if (this._healthAmount < _maxHealth / 3 && !_boss.IsEnraged)
        {
            _animator.SetTrigger("Enrage");
        }
    }

    protected override void Die()
    {
        _collider.enabled = false;
        _animator.SetTrigger("Die");
        Camera.main.GetComponent<ScreenShake>().Shake(3.1f, 0.15f);
    }
}

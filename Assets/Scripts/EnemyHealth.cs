using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Damagable
{
    [SerializeField] protected GameObject _deathParticlesPrefab;
    protected EnemyAudio _audio;
    protected Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<EnemyAudio>();
    }

    public override void TakeDamage(float amount)
    {
        this._healthAmount -= amount * 10f;
        this._animator.SetTrigger("Hurt");

        if (this._healthAmount <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        if (_deathParticlesPrefab)
            Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
        Camera.main.GetComponent<ScreenShake>().Shake(0.15f, 0.07f);
        _audio.Die();
        Destroy(this.gameObject);
    }
}

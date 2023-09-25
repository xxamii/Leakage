using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damagable
{
    [SerializeField] private GameObject _deathParticlesPrefab;
    [SerializeField] private BoxCollider2D _damageCollider;
    [SerializeField] private GameObject _spritesContainer;
    private PlayerMovement _movement;
    private PlayerAnimator _animator;
    private PlayerInput _input;
    private PlayerAudio _audio;
    private bool _isAlive = true;

    public float HealthAmount => this._healthAmount;

    private float _safeTime = 1f;

    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<PlayerAnimator>();
        _input = GetComponent<PlayerInput>();
        _audio = GetComponent<PlayerAudio>();
    }


    public override void TakeDamage(float amount)
    {
        this._healthAmount -= amount;
        _animator.Hurt();
        _audio.Hurt();

        UIManager.Instance.ShowHealth((int)this._healthAmount);

        if (this._healthAmount <= 0)
        {
            Die();
        }

        StartCoroutine(SafeRoutine());
    }

    protected override void Die()
    {
        _isAlive = false;
        if (_deathParticlesPrefab)
            Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
        _audio.Die();
        StartCoroutine(RestartRoutine());
        Camera.main.GetComponent<ScreenShake>().Shake(0.15f, 0.15f);
    }

    private IEnumerator SafeRoutine()
    {
        _damageCollider.enabled = false;
        yield return new WaitForSeconds(_safeTime);

        if (_isAlive)
            _damageCollider.enabled = true;
    }

    private IEnumerator RestartRoutine()
    {
        StopCoroutine(SafeRoutine());
        _input.Die();
        _damageCollider.enabled = false;
        _spritesContainer.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneLoader.Restart();
    }
}

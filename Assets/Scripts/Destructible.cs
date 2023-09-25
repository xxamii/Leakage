using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : Damagable
{
    [SerializeField] private GameObject[] _destroyParticlesPrefabs;
    [SerializeField] private GameObject _corpse;
    [SerializeField] private AudioClip _destroySound;
    private bool _isDead = false;

    public override void TakeDamage(float amount)
    {
        if (!_isDead)
            Die();
    }

    protected override void Die()
    {
        _isDead = true;

        if (_destroyParticlesPrefabs.Length > 0)
        {
            foreach (GameObject particles in _destroyParticlesPrefabs)
            {
                Instantiate(particles, transform.position, particles.transform.rotation);
            }
        }

        AudioSource.PlayClipAtPoint(_destroySound, transform.position);

        if (_corpse)
            Instantiate(_corpse, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _deathSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        _audioSource.PlayOneShot(_stepSound);
    }

    public void Die()
    {
        _audioSource.PlayOneShot(_deathSound);
    }
}

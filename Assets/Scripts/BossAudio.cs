using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _landSound;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _takeofSound;
    [SerializeField] private AudioClip _roarSound;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Land()
    {
        _audioSource.PlayOneShot(_landSound);
    }

    public void Shoot()
    {
        _audioSource.PlayOneShot(_shotSound, 0.6f);
    }

    public void Jump()
    {
        _audioSource.PlayOneShot(_takeofSound, 0.2f);
    }

    public void Roar()
    {
        _audioSource.PlayOneShot(_roarSound);
    }
}

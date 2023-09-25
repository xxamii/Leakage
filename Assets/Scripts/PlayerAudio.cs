using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _dieSplashSound, _dieVoiceSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        _audioSource.PlayOneShot(_stepSound, 0.4f);
    }

    public void Hurt()
    {
        _audioSource.PlayOneShot(_hurtSound);
    }

    public void Die()
    {
        _audioSource.PlayOneShot(_dieSplashSound);
        _audioSource.PlayOneShot(_dieVoiceSound);
    }
}

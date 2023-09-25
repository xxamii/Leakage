using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioClip _clickSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Hover()
    {
        _audioSource.PlayOneShot(_hoverSound);
    }

    public void Click()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}

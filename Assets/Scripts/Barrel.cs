using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private AudioClip _hitSound, _fallSound;
    private Animator _animator;
    private Transform _transform;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = this.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_transform.position.x > collision.transform.position.x)
        {
            // Collision from the left, fall right
            _animator.SetTrigger("FallRight");
        }
        else
        {
            // Collision from the right, fall left
            _animator.SetTrigger("FallLeft");
        }

        AudioSource.PlayClipAtPoint(_hitSound, transform.position);
        _collider.enabled = false;
    }

    public void PlayFallSound()
    {
        AudioSource.PlayClipAtPoint(_fallSound, transform.position);
    }
}

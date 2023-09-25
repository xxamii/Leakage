using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    // Dependecies
    private Animator _animator;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private GameObject _buttonImage;
    [SerializeField] private AudioClip _switchSound;
    private Animator _buttonAnimator;

    // Values
    private bool _isPlayerStaying = false;
    [SerializeField] private int _levelId;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _buttonAnimator = _buttonImage.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isPlayerStaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _buttonAnimator.SetBool("IsActive", false);
                _animator.SetBool("IsSwitched", true);
                AudioSource.PlayClipAtPoint(_switchSound, transform.position);
                _collider.enabled = false;
                GameState.Instance.LeverSwitched(_levelId);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            _isPlayerStaying = true;
            _buttonAnimator.SetBool("IsActive", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            _isPlayerStaying = false;
            _buttonAnimator.SetBool("IsActive", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetPositionA, _targetPositionB;
    [SerializeField] private AudioClip _startMovementSound;
    private AudioSource _audioSource;
    private Transform _currentTarget;
    private Transform _transform;

    private bool _isMoving = false;
    private float _speed = 5f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _transform = this.transform;
        _currentTarget = _targetPositionB;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            collision.transform.parent = _transform;

            if (!_isMoving)
            {
                DefinePosition();
                StartMoving();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            collision.transform.parent = null;
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, _currentTarget.position, _speed * Time.fixedDeltaTime);

            if (_transform.position == _currentTarget.position)
            {
                _isMoving = false;
                AudioSource.PlayClipAtPoint(_startMovementSound, transform.position);
                _audioSource.Stop();
            }
        }
    }

    private void StartMoving()
    {
        _isMoving = true;
        AudioSource.PlayClipAtPoint(_startMovementSound, transform.position);
        _audioSource.Play();
    }

    private void DefinePosition()
    {
        if (_transform.position == _targetPositionA.position)
        {
            _currentTarget = _targetPositionB;
        }
        else if (_transform.position == _targetPositionB.position)
        {
            _currentTarget = _targetPositionA;
        }
    }

    public void Call(Vector3 targetPosition)
    {
        if (_transform.position != targetPosition && !_isMoving)
        {
            DefinePosition();
            StartMoving();
        }
    }
}

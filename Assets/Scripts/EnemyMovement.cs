using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Dependecies
    [SerializeField] private Transform _groundCheck, _leg1, _leg2, _wallCheck;
    [SerializeField] private LayerMask _whatIsGround;
    private Transform _transform;
    private Rigidbody2D _rigidBody;

    // Values
    [SerializeField] private float _speed = 3f;
    private float _groundExistCheckLength = 0.2f;
    private float _groundCheckLength = 0.1f;
    private float _wallCheckLength = 0.4f;
    private float _knockBackFactor = 6f;

    // State
    private bool _isChasing = false;
    private bool _isGrounded;
    [SerializeField] private bool _isLookingRight = true;
    private bool _isBeingKnocked = false;

    private void Start()
    {
        _transform = this.transform;
        _rigidBody = GetComponent<Rigidbody2D>();
        _isLookingRight = _transform.eulerAngles.y < 90f;

        ResetVelocity();
    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();
    }

    private void GroundCheck()
    {
        RaycastHit2D groundCheckRay1 = Physics2D.Raycast(_leg1.position, -Vector2.up, _groundCheckLength, _whatIsGround.value);
        RaycastHit2D groundCheckRay2 = Physics2D.Raycast(_leg2.position, -Vector2.up, _groundCheckLength, _whatIsGround.value);

        if (groundCheckRay1 || groundCheckRay2)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private void Move()
    {
        RaycastHit2D groundExistRay = Physics2D.Raycast(_groundCheck.position, -Vector2.up, _groundExistCheckLength, _whatIsGround.value);
        RaycastHit2D wallRay = Physics2D.Raycast(_wallCheck.position, _transform.right, _wallCheckLength, _whatIsGround.value);

        if (!_isChasing)
        {
            if ((!groundExistRay || wallRay) && _isGrounded)
            {
                Flip();
            }
        }
        else
        {
            if (Player.Instance)
            {
                Vector2 playerPosition = Player.Instance.transform.position;

                if (playerPosition.x > _transform.position.x && !_isLookingRight || playerPosition.x < _transform.position.x && _isLookingRight)
                {
                    Flip();
                }
            }

            if (!_isBeingKnocked)
            {
                ResetVelocity();
            }
        }
    }

    private void Flip()
    {
        _transform.Rotate(0, 180, 0);
        _isLookingRight = !_isLookingRight;
        ResetVelocity();
    }

    public void Chase()
    {
        if (!_isChasing)
        {
            _isChasing = true;
            ResetVelocity();
        }
    }

    public void Patrol()
    {
        if (_isChasing)
        {
            _isChasing = false;
            ResetVelocity();
        }
    }

    private void ResetVelocity()
    {
        _rigidBody.velocity = _transform.right * _speed;
    }

    public void Knockback(Vector2 direction)
    {
        StartCoroutine(KnockbackRoutine(direction));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        _isBeingKnocked = true;
        _rigidBody.velocity = direction * _knockBackFactor;

        yield return new WaitForSeconds(0.3f);

        _rigidBody.velocity = _transform.right * _speed;
        _isBeingKnocked = false;
    }
}

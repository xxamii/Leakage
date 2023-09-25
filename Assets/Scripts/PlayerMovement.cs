using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Dependecies
    [SerializeField] private Transform _groundCheck1, _groundCheck2;
    private Rigidbody2D _rigidBody;
    private Transform _transform;
    private PlayerInput _input;
    private PlayerAnimator _playerAnimator;
    [SerializeField] private LayerMask _whatIsGround;

    // Values
    private float _speed = 6f;
    private float _jumpVelocity = 15f;
    private float _lowJumpMultiplier = 0.4f;
    private float _groundDisctance = 0.1f;
    private float _knockBackFactor = 10f;

    // State
    private bool _isGrounded;
    [SerializeField] private bool _isFacingRight = true;
    private float _delayedJumpTime = 0.12f;
    private float _delayedJump;
    private float _coyoteTime = 0.1f;
    private float _coyote;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = this.transform;
        _input = GetComponent<PlayerInput>();
        _playerAnimator = GetComponent<PlayerAnimator>();

        if (CheckpointManager.Instance.IsCheckpointSet())
            _transform.position = CheckpointManager.Instance.CurrentCheckpoint();

        _delayedJump = 0f;
        _coyote = 0f;
    }

    private void FixedUpdate()
    {
        GroundCheck();
        FallCheck();
    }

    private void Update()
    {
        _delayedJump -= Time.deltaTime;
        _coyote -= Time.deltaTime;

        if (_isGrounded)
            _coyote = _coyoteTime;
    }

    private void FallCheck()
    {
        if (!_isGrounded && _rigidBody.velocity.y < 0)
        {
            _playerAnimator.Fall();
        }
        else if (!_isGrounded && _rigidBody.velocity.y > 0)
        {
            _playerAnimator.Jump();
        }
        else
        {
            _playerAnimator.Land();
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.Raycast(_groundCheck1.position, -Vector2.up, _groundDisctance, _whatIsGround.value) || Physics2D.Raycast(_groundCheck2.position, -Vector2.up, _groundDisctance, _whatIsGround.value))
        {
            _isGrounded = true;
        } 
        else
        {
            _isGrounded = false;
        }
    }

    public void Move(int direction)
    {
        Flip(direction);

        _playerAnimator.Walk(direction, _isGrounded);

        float xMovement = direction * _speed;
        _rigidBody.velocity = new Vector2(xMovement, _rigidBody.velocity.y);
    }

    private void Flip(float direction)
    {
        if (direction > 0 && !_isFacingRight || direction < 0 && _isFacingRight)
        {
            _isFacingRight = !_isFacingRight;

            _transform.Rotate(0f, 180f, 0f);
        }
    }

    public void Jump(ref bool vertical)
    {
        if (vertical)
        {
            _delayedJump = _delayedJumpTime;

            vertical = false;
        }

        if (_delayedJump > 0f && _coyote > 0f)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpVelocity);
            _delayedJump = 0;
            _playerAnimator.TakeOf();
        }
    }

    public void CutJump()
    {
        if (_rigidBody.velocity.y > 0)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y * _lowJumpMultiplier);
        }
    }

    public void Knockback(Vector2 direction)
    {
        StartCoroutine(KnockbackRoutine(direction));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        _input.SetKnockback(true);

        _rigidBody.velocity = direction * _knockBackFactor;

        yield return new WaitForSeconds(0.2f);

        _rigidBody.velocity = Vector2.zero;

        _input.SetKnockback(false);
    } 
}

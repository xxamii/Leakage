using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Player _player;
    private Transform _transform;
    private bool _isLookingRight = false;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool _isJumping;
    [SerializeField] private Transform _groundCheck;
    private float _groundCheckLength = 0.2f;
    [SerializeField] private LayerMask _whatIsGround;
    private BossAudio _audio;
    private bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<BossAudio>();
        _player = Player.Instance;
        _transform = this.transform;
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        RaycastHit2D groundCheckRay = Physics2D.Raycast(_groundCheck.position, -Vector2.up, _groundCheckLength, _whatIsGround.value);

        if (groundCheckRay)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    public void LookAtPlayer()
    {
        if (_player.transform.position.x < _transform.position.x && _isLookingRight)
        {
            // Player is on the left
            Flip();
        }
        else if (_player.transform.position.x >= _transform.position.x && !_isLookingRight)
        {
            // Player is on the right
            Flip();
        }
    }

    private void Flip()
    {
        _transform.Rotate(0, 180f, 0);
        _isLookingRight = !_isLookingRight;
    }

    public void Jump()
    {
        Transform player = Player.Instance.transform;

        Vector2 difference = player.position - _transform.position;

        var angle = Mathf.Atan((difference.y + 4.905f) / difference.x);

        float totalVelocity = difference.x / Mathf.Cos(angle);
        float vx = totalVelocity * Mathf.Cos(angle);
        float vy = totalVelocity * Mathf.Sin(angle);

        _rigidBody.velocity = new Vector2(vx / 2.15f, vy * 2.15f);

        _audio.Jump();

        _isJumping = true;

        _animator.SetBool("IsJumping", true);
    }

    public void Land()
    {
        if (_isJumping && _isGrounded)
        {
            _isJumping = false;
            _animator.SetBool("IsJumping", false);
            _audio.Land();
            _rigidBody.velocity = Vector2.zero;
            Camera.main.GetComponent<ScreenShake>().Shake(0.15f, 0.1f);
        }
    }
}

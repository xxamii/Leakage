using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Dependecies
    private PlayerMovement _movement;
    private PlayerShooter _shooter;

    // Values
    private int _horizontalInput;
    private bool _jumpInput;

    // State
    private bool _isBeingKnocked = false;
    private bool _isAlive = true;

    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _shooter = GetComponent<PlayerShooter>();
    }

    private void Update()
    {
        if (_isAlive)
        {
            GetVerticalInput();
            JumpInput();
            ShootInput();
        }
    }

    private void FixedUpdate()
    {
        if (!_isBeingKnocked && _isAlive)
        {
            _movement.Move(_horizontalInput);
            _movement.Jump(ref _jumpInput);
        }
    }

    private void GetVerticalInput()
    {
        _horizontalInput = (int)Input.GetAxisRaw("Horizontal");
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpInput = true;
        } else if (Input.GetButtonUp("Jump"))
        {
            _movement.CutJump();
        }
    }

    private void ShootInput()
    {
        if (Input.GetButton("Fire1"))
        {
            _shooter.Shoot();
        }
    }

    public void SetKnockback(bool state)
    {
        _isBeingKnocked = state;
    }

    public void Die()
    {
        _isAlive = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Walk(int direction, bool isGrounded)
    {
        if (isGrounded)
            _animator.SetBool("IsWalking", direction != 0);
    }

    public void Shoot()
    {
        _animator.SetTrigger("Shoot");
    }

    public void TakeOf()
    {
        _animator.SetTrigger("TakeOf");
        _animator.SetBool("IsJumping", true);
    }

    public void Fall()
    {
        _animator.SetBool("IsJumping", false);
        _animator.SetBool("IsFalling", true);
    }

    public void Jump()
    {
        _animator.SetBool("IsJumping", true);
    }

    public void Land()
    {
        _animator.SetBool("IsJumping", false);
        _animator.SetBool("IsFalling", false);
    }

    public void Hurt()
    {
        _animator.SetTrigger("Hurt");
    }
}

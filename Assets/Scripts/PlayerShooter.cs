using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    // Dependecies
    private IGun _gun;
    private PlayerAnimator _playerAnimator;

    // Values
    private float _shootDelay;
    private float _shootDelayTime = 0.25f;

    private void Start()
    {
        _gun = GetComponent<IGun>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _shootDelay = _shootDelayTime;
    }

    public void Shoot()
    {
        if (Time.time > _shootDelay)
        {
            _gun.Shoot();
            // _playerAnimator.Shoot();
            _shootDelay = Time.time + _shootDelayTime;
        }
    }
}

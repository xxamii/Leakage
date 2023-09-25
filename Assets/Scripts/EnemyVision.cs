using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    // Dependecies
    [SerializeField] private LayerMask _whatIsEnemy;
    private EnemyMovement _movement;
    private Transform _transform;

    // Values
    private float _lineOfSight = 6f;

    // State
    private bool _isPlayerSeen;

    private void Start()
    {
        _movement = GetComponent<EnemyMovement>();
        _transform = this.transform;
    }

    private void FixedUpdate()
    {
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        RaycastHit2D rightPlayerRay = Physics2D.Raycast(_transform.position, _transform.right, _lineOfSight, _whatIsEnemy.value);
        RaycastHit2D leftPlayerRay = Physics2D.Raycast(_transform.position, -_transform.right, _lineOfSight, _whatIsEnemy.value);

        if (rightPlayerRay || leftPlayerRay)
        {
            _isPlayerSeen = true;
            _movement.Chase();
        } else
        {
            _isPlayerSeen = false;
            _movement.Patrol();
        }
    }
}

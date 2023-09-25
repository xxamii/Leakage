using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    private Boss _boss;
    private BossMovement _movement;
    private Player _player;
    private Transform _transform;
    private float _minIdleTime, _maxIdleTime, _idleTimer;
    private int _minAttackId, _maxAttackId;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
        _movement = animator.GetComponent<BossMovement>();
        _player = Player.Instance;
        _transform = animator.GetComponent<Transform>();

        if (!_boss.IsEnraged)
        {
            _minIdleTime = 2.2f;
            _maxIdleTime = 3f;
            _minAttackId = 1;
            _maxAttackId = 3;
        }
        else
        {
            _minIdleTime = 2f;
            _maxIdleTime = 2.7f;
            _minAttackId = 1;
            _maxAttackId = 4;
        }
        
        _idleTimer = Random.Range(_minIdleTime, _maxIdleTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _movement.LookAtPlayer();
        _idleTimer -= Time.deltaTime;

        if (_idleTimer <= 0f)
        {
            if (Vector2.Distance(_transform.position, _player.transform.position) < 4.7f)
            {
                _boss.ChooseAttack(0, 0);
            }
            else
            {
                _boss.ChooseAttack(_minAttackId, _maxAttackId);
            }

            _idleTimer = 10f;
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

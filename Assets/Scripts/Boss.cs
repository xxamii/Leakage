using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private BossMovement _movement;
    private Rigidbody2D _rigidBody;
    private bool _started = false;
    [SerializeField] private ArenaDoor _arenaDoor;

    private enum Attack
    {
        AttackMelee,
        AttackShoot,
        AttackJump,
        AttackSpawn
    }

    private Animator _animator;

    public bool IsEnraged { get; private set; }

    private void Start()
    {
        _movement = GetComponent<BossMovement>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        IsEnraged = false;
    }

    public void ChooseAttack(int min, int max)
    {
        Attack attack = (Attack)Random.Range(min, max);
        _animator.SetTrigger(attack.ToString());
    }

    public void Enrage()
    {
        IsEnraged = true;
        Camera.main.GetComponent<ScreenShake>().Shake(2.1f, 0.1f);
    }

    public void StartBossFight()
    {
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_started && collision.gameObject.layer == 15)
        {
            _started = true;
            _animator.SetTrigger("Appear");
            Camera.main.GetComponent<ScreenShake>().Shake(0.2f, 0.15f);
        }
    }

    public void EndBossFight()
    {
        _arenaDoor.Open();
        Camera.main.GetComponent<ScreenShake>().Shake(0.2f, 0.15f);
    }

    public void ScreenShakeAppearance()
    {
        Camera.main.GetComponent<ScreenShake>().Shake(2f, 0.1f);
    }
}

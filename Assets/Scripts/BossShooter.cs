using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private GameObject _particlesPrefab;
    private BossAudio _audio;
    private Transform _transform;

    private void Start()
    {
        _transform = this.transform;
        _audio = GetComponent<BossAudio>();
    }

    public void Shoot()
    {
        Transform player = Player.Instance.transform;

        Vector2 difference = player.position - _shotPoint.position;

        var angle = Mathf.Atan((difference.y + 4.905f) / difference.x);

        float totalVelocity = difference.x / Mathf.Cos(angle);
        float vx = totalVelocity * Mathf.Cos(angle);
        float vy = totalVelocity * Mathf.Sin(angle);

        GameObject bullet = Instantiate(_bulletPrefab, _shotPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(vx, vy);
        Instantiate(_particlesPrefab, _shotPoint.position, Quaternion.identity);

        _audio.Shoot();
    }
}

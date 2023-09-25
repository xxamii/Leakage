using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _particlesPrefab;
    private BossAudio _audio;

    private void Start()
    {
        _audio = GetComponent<BossAudio>();
    }

    public void Spawn()
    {
        Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.Euler(0, transform.eulerAngles.y + 180f, 0));
        Instantiate(_particlesPrefab, _spawnPoint.position, Quaternion.identity);
        _audio.Shoot();
    }
}

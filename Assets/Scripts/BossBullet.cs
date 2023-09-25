using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

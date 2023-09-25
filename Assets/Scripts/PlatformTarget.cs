using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTarget : MonoBehaviour
{
    [SerializeField] private PlatformMovement _platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            _platform.Call(transform.position);
        }
    }
}

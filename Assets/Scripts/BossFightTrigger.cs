using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    private bool _isBossFightStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && !_isBossFightStarted)
        {
            _boss.StartBossFight();
            _isBossFightStarted = true;
        }
    }
}

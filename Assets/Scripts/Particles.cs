using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    void Start()
    {
        Destroy(this.gameObject, _destroyTime);
    }
}

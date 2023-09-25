using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoor : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AudioClip _slideSound;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Close();
    }

    public void Close()
    {
        _animator.SetTrigger("Close");
        AudioSource.PlayClipAtPoint(_slideSound, transform.position);
    }

    public void Open()
    {
        _animator.SetTrigger("Open");
        AudioSource.PlayClipAtPoint(_slideSound, transform.position);
    }
}

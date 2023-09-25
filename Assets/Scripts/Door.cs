using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private static List<int> _closedDoorsIds = new List<int>();

    [SerializeField] private int _levelId;
    [SerializeField] private AudioClip _slideSound;
    private Animator _animator;
    private bool _isClosed;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _isClosed = _closedDoorsIds.Contains(_levelId);

        if (_isClosed)
        {
            if (!GameState.Instance.IsLevelClosed(_levelId))
            {
                Open();
            }
            else
            {
               _animator.SetBool("IsClosed", true);
            }
        }
        else
        {
            if (GameState.Instance.IsLevelClosed(_levelId))
            {
                Close();
            }
        }
    }

    private void Close()
    {
        _closedDoorsIds.Add(_levelId);
        _isClosed = true;
        _animator.SetTrigger("Close");
        AudioSource.PlayClipAtPoint(_slideSound, transform.position);
    }

    private void Open()
    {
        _closedDoorsIds.Remove(_levelId);
        _isClosed = false;
        _animator.SetTrigger("Open");
        AudioSource.PlayClipAtPoint(_slideSound, transform.position);
    }
}

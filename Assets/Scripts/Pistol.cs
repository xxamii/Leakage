using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IGun
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gunTip;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _reloadSound;
    private AudioSource _audioSource;
    private UIManager _uiManager;

    [SerializeField] private int _bulletsCapacity = 5;
    private int _bulletsAmount;

    private bool _isReloading = false;

    private void Start()
    {
        _bulletsAmount = _bulletsCapacity;
        _uiManager = UIManager.Instance;
        _uiManager.ShowBulletsAmount(_bulletsAmount);
        _audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (_bulletsAmount > 0 && !_isReloading)
        {
            _audioSource.PlayOneShot(_shotSound, 0.3f);
            Instantiate(_bulletPrefab, _gunTip.position, _gunTip.rotation);
            _bulletsAmount--;
            _uiManager.ShowBulletsAmount(_bulletsAmount);

            if (_bulletsAmount <= 0)
            {
                _isReloading = true;
                StartCoroutine(ReloadRoutine());
            }
        }
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        _audioSource.PlayOneShot(_reloadSound, 0.2f);
        yield return new WaitForSeconds(0.45f);
        _bulletsAmount = _bulletsCapacity;
        _isReloading = false;
        _uiManager.ShowBulletsAmount(_bulletsAmount);
    }
}

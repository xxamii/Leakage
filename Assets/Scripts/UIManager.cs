using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _heartsImages;
    [SerializeField] private Text _bulletsAmountText;
    private GameObject _player;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _player = Player.Instance.gameObject;

        if (_player.GetComponent<PlayerHealth>())
        {
            ShowHealth((int)_player.GetComponent<PlayerHealth>().HealthAmount);
        }
    }

    public void ShowHealth(int amount)
    {
        for (int i = 0; i < _heartsImages.Length; i++)
        {
            if (amount > i)
            {
                _heartsImages[i].SetActive(true);
            }
            else
            {
                _heartsImages[i].SetActive(false);
            }
        }
    }

    public void ShowBulletsAmount(int amount)
    {
        _bulletsAmountText.text = amount.ToString();
    }
}

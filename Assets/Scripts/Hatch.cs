using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    // Dependecies
    [SerializeField] private GameObject _buttonImage;
    [SerializeField] private Sprite[] _lightsImages;
    private Animator _buttonAnimator;
    [SerializeField] private GameObject _lightsPanel;
    [SerializeField] private GameObject _sceneTransitionCanvas;
    [SerializeField] private AudioClip _hatchSound;
    private AudioSource _audioSource;

    // State
    private bool _isOpened;
    private int _leversSwitchedAmount;
    private bool _isPlayerStaying;

    // Values
    private int _leversAmount = 2;

    private void Start()
    {
        _leversSwitchedAmount = GameState.Instance.LeverSwitchedAmount;
        _buttonAnimator = _buttonImage.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _isOpened = _leversSwitchedAmount == _leversAmount;

        _lightsPanel.GetComponent<SpriteRenderer>().sprite = _lightsImages[_leversSwitchedAmount];
    }

    private void Update()
    {
        if (_isPlayerStaying && _isOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _buttonAnimator.SetBool("IsActive", false);
                StartCoroutine(LoadScene());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && _isOpened)
        {
            _isPlayerStaying = true;
            _buttonAnimator.SetBool("IsActive", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && _isOpened)
        {
            _isPlayerStaying = false;
            _buttonAnimator.SetBool("IsActive", false);
        }
    }

    private IEnumerator LoadScene()
    {
        _sceneTransitionCanvas.GetComponent<Animator>().SetTrigger("FadeOut");
        _audioSource.PlayOneShot(_hatchSound);

        yield return new WaitForSeconds(1.6f);

        SceneLoader.LoadScene(SceneLoader.Scene.BossFightArena);
    }
}

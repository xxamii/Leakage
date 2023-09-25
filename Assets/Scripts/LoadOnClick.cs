using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scene _sceneToLoad;
    [SerializeField] private GameObject _sceneTransitionCanvas;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(Load);
    }

    private void Load()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        _sceneTransitionCanvas.GetComponent<Animator>().SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        SceneLoader.LoadScene(_sceneToLoad);
    }
}

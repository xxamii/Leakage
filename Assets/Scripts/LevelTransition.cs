using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scene _sceneToLoad;
    [SerializeField] private GameObject _sceneTransitionCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            StartCoroutine(Load());
        }
    }

    private IEnumerator Load()
    {
        _sceneTransitionCanvas.GetComponent<Animator>().SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        SceneLoader.LoadScene(_sceneToLoad);
    }
}

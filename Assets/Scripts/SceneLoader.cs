using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        LevelZero,
        LevelOne,
        LevelTwo,
        BossFightArena,
        MainMenu,
        Endgame
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static int CurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}

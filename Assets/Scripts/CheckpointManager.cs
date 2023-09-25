using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static int _lastSceneId;
    private static Vector2 _currentCheckpoint;

    private static CheckpointManager _instance;
    public static CheckpointManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        int currentSceneId = SceneLoader.CurrentScene();

        if (currentSceneId != _lastSceneId)
        {
            _currentCheckpoint = Vector2.zero;
            _lastSceneId = currentSceneId;
        }
    }

    public Vector2 CurrentCheckpoint()
    {
        return _currentCheckpoint;
    }

    public void SetCurrentCheckpoint(Vector2 checkpoint)
    {
        _currentCheckpoint = checkpoint;
    }

    public bool IsCheckpointSet()
    {
        return _currentCheckpoint != Vector2.zero;
    }
}

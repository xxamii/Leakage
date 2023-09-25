using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private List<int> _closedLevelIds = new List<int>();

    private int _leversAmount = 2;

    public int LeverSwitchedAmount { get; private set; }

    private static GameState _instance;
    public static GameState Instance {
        get {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            _closedLevelIds.Add(2);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LeverSwitched(int id)
    {
        if (LeverSwitchedAmount < _leversAmount)
        {
            LeverSwitchedAmount++;
            _closedLevelIds.Add(id);

            if (id == 1)
            {
                _closedLevelIds.Remove(2);
            }
        }
    }

    public bool IsLevelClosed(int id)
    {
        return _closedLevelIds.Contains(id);
    }
}

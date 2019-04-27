using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITickable : MonoBehaviour
{
    protected GameTimeManager _timeManager;

    private void OnEnable()
    {
        _timeManager = GameObject.Find("GameMaster").GetComponent<GameTimeManager>();
        _timeManager.Register(this);
    }

    private void OnDisable()
    {
        _timeManager.Unregister(this);
    }

    public abstract void Tick();
}

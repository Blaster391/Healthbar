using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : ScriptableObject
{
    [SerializeField]
    private string _patternString = "0";

    [SerializeField]
    private int _cooldown = 1;

    [SerializeField]
    private BaseAction _upgradedAction = null;

    private int _currentTime = 0;

    private bool[] _actionPattern;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Tick()
    {
        if(_currentTime >= 0)
        {
            _currentTime -= 1;
        }
    }

    public bool IsActive()
    {
        return _currentTime <= 0;
    }

    public void Trigger()
    {
        if (!IsActive())
        {
            return;
        }

        TriggerCore();
        _currentTime = _cooldown;
    }

    protected abstract void TriggerCore();

    private void ParsePattern()
    {
        GameTimeManager timeManager = GameMaster.Find<GameTimeManager>();

        _actionPattern = new bool[timeManager.TotalBeats];
    }
}

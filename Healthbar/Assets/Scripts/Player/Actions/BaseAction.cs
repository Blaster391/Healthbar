using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseAction : ScriptableObject
{

    protected PlayerScript Player { get { return GameMaster.Find<PlayerScript>(); } }
    protected EnemyScript Enemy { get { return GameMaster.Find<EnemyScript>(); } }

    [SerializeField]
    private string _actionName = "";
    public string ActionName { get { return _actionName; } }

    [SerializeField]
    private string _patternString = "R";

    [SerializeField]
    private int _cooldown = 1;

    [SerializeField]
    private BaseAction _upgradedAction = null;

    private int _currentTime = 0;

    private InputType[] _actionPattern;

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
        PutOnCooldown();
    }

    public void ResetCooldown()
    {
        _currentTime = 0;
    }

    public void PutOnCooldown()
    {
        _currentTime = _cooldown;
    }

    protected abstract void TriggerCore();

    public void ParsePattern()
    {
        _actionPattern = new InputType[_patternString.Length];

        for(int i = 0; i < _patternString.Length; ++i)
        {
            if(_patternString.Equals("L", System.StringComparison.CurrentCultureIgnoreCase))
            {
                _actionPattern[i] = InputType.Left;
            }
            else if(_patternString.Equals("R", System.StringComparison.CurrentCultureIgnoreCase))
            {
                _actionPattern[i] = InputType.Right;
            }
            else
            {
                Debug.LogError("Josh wtf");
            }
        }
    }

    public bool IsValidPartialInput(InputType[] inputPattern)
    {
        if(_actionPattern.Length < inputPattern.Length)
        {
            return false;
        }

        for(int i = 0; i < inputPattern.Length; ++i)
        {
            if(inputPattern[i] != _actionPattern[i])
            {
                return false;
            }
        }

        return true;
    }

    public bool IsValidInput(InputType[] inputPattern)
    {
        return inputPattern.SequenceEqual(_actionPattern);
    }
}

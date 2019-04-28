using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionType
{
    Attack,
    Defence
}

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
    public int CooldownLength => _cooldown;
    public int CooldownRemaining => _currentTime;

    [SerializeField]
    private int _purchaseCost = 1;
    public int PurchaseCost => _purchaseCost;

    private int _currentTime = 0;

    private InputType[] _actionPattern;
    public InputType[] ActionPattern => _actionPattern;

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
        if (IsActive())
        {
            TriggerCore();
        }

        
        PutOnCooldown();
    }

    public void ResetCooldown()
    {
        _currentTime = 0;
    }

    public void PutOnCooldown()
    {
        _currentTime = _cooldown + 1;
    }

    protected abstract void TriggerCore();
    public abstract ActionType ActionType();

    public void ParsePattern()
    {
        _actionPattern = new InputType[_patternString.Length];

        for(int i = 0; i < _patternString.Length; ++i)
        {
            if(_patternString[i].Equals('L'))
            {
                _actionPattern[i] = InputType.Left;
            }
            else if(_patternString[i].Equals('R'))
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

    public abstract string EffectText();
}

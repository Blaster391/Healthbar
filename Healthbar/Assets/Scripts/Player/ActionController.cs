﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : ITickable
{
    [SerializeField]
    private List<BaseAction> _actions;

    private InputController _inputController;

    private List<InputType> _currentInputs = new List<InputType>();

    private void Start()
    {
        _inputController = GameMaster.Find<InputController>();
        foreach(var action in _actions)
        {
            action.ParsePattern();
        }
    }

    private void Update()
    {
        MonitorInputs();
    }

    private void MonitorInputs()
    {


        if (_inputController.LeftPressed())
        {
            _currentInputs.Add(InputType.Left);
        }

        if (_inputController.RightPressed())
        {
            _currentInputs.Add(InputType.Right);
        }

        if (_inputController.ConfirmPressed())
        {
            AttemptActions();
        }

        CheckActionsStillValid();
    }

    public void AttemptActions()
    {
        bool valid = false;
        foreach (var action in _actions)
        {
            if (action.IsValidInput(_currentInputs.ToArray()))
            {
                Debug.Log("Triggered " + action.ActionName);
                action.Trigger();
                valid = true;
            }
        }

        _currentInputs.Clear();
        //TODO events for UI
        if (valid)
        {

        }
        else
        {

        }
    }

    public void CheckActionsStillValid()
    {
        foreach(var action in _actions)
        {
            if (action.IsValidPartialInput(_currentInputs.ToArray()))
            {
                return;
            }
        }

        //Do invalid actions
        Debug.Log("No valid actions");
        foreach (var action in _actions)
        {
            action.PutOnCooldown();
        }
        _currentInputs.Clear();
        //TODO fire event for sound and animation
    }

    public override void Tick()
    {
        foreach (var action in _actions)
        {
            action.Tick();
        }
    }
}

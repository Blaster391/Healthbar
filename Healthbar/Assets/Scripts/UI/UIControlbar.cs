﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControlbar : MonoBehaviour
{
    [SerializeField]
    private Image _leftControl;
    [SerializeField]
    private Sprite _leftControlDefault;
    [SerializeField]
    private Sprite _leftControlPressed;


    [SerializeField]
    private Image _rigthControl;
    [SerializeField]
    private Sprite _rightControlDefault;
    [SerializeField]
    private Sprite _rightControlPressed;

    [SerializeField]
    private Image _confirmControl;
    [SerializeField]
    private Sprite _confirmControlDefault;
    [SerializeField]
    private Sprite _confirmControlPressed;
    [SerializeField]
    private Sprite _confirmControlDisabled;


    private InputController _inputController;
    private ActionController _actionController;
    void Start()
    {
        _inputController = GameMaster.Find<InputController>();
        _actionController = GameMaster.Find<ActionController>();
    }

    // Update is called once per frame
    bool _confirmed = false;

    void Update()
    {
        if (_inputController.LeftHeld())
        {
            _leftControl.sprite = _leftControlPressed;
        }
        else
        {
            _leftControl.sprite = _leftControlDefault;
        }

        if (_inputController.RightHeld())
        {
            _rigthControl.sprite = _rightControlPressed;
        }
        else
        {
            _rigthControl.sprite = _rightControlDefault;
        }

        if(_actionController.CurrentInputs.Count > 0 || _confirmControl.sprite != _confirmControlDisabled || _confirmed)
        {
 

  
            if(_actionController.CurrentInputs.Count > 0 && !_confirmed)
            {
                _confirmControl.sprite = _confirmControlDefault;
            }else if (!_confirmed)
            {
                _confirmControl.sprite = _confirmControlDisabled;
            }

            if (_inputController.ConfirmHeld() || _inputController.ConfirmPressed())
            {
                _confirmControl.sprite = _confirmControlPressed;
                _confirmed = true;
            }
            else
            {
                _confirmed = false;
            }


        }
        else
        {
            _confirmControl.sprite = _confirmControlDisabled;
        }
    }
}

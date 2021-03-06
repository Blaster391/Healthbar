﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Left,
    Right,
    Confirm
}

public class InputController : MonoBehaviour
{
    public bool ConfirmPressed()
    {
        return Input.GetButtonDown("Confirm");
    }

    public bool LeftPressed()
    {
        return Input.GetButtonDown("Left");
    }

    public bool RightPressed()
    {
        return Input.GetButtonDown("Right");
    }

    public bool ConfirmHeld()
    {
        return Input.GetButton("Confirm");
    }

    public bool LeftHeld()
    {
        return Input.GetButton("Left");
    }

    public bool RightHeld()
    {
        return Input.GetButton("Right");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUp
{
    public string description;
    private float value;
    private Action<float> eventCall;


    public PowerUp(string d, float f, Action<float> ec)
    {
        description = d;
        value = f;
        eventCall = ec;
    }

    public void CallEvent()
    {
        eventCall.Invoke(value);
    }
}

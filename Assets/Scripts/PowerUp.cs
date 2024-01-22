using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp
{
    public string description;
    public Action<float> eventCall;

    public PowerUp(string d, Action<float> ec)
    {
        description = d;
        eventCall = ec;
    }
}

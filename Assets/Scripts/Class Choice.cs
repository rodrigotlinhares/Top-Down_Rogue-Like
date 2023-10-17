using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClassChoice : MonoBehaviour
{
    public Color color;
    public static Action<string> OnClick;

    public void Enable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void Disable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChooseClass()
    {
        OnClick(name);
    }
}

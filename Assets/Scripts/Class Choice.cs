using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassChoice : MonoBehaviour
{
    public Color color;
    public delegate void Click(string s);
    public static event Click OnClick;

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

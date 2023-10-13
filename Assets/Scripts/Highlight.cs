using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Color color;

    public void Enable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void Disable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

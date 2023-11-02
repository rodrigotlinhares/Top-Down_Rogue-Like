using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
    private int duration = 250;

    public IEnumerator ChangeColor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color oldColor = sprite.color;
        sprite.color = Color.red;
        float rStep = (oldColor.r - Color.red.r) / duration;
        float gStep = (oldColor.g - Color.red.g) / duration;
        float bSTep = (oldColor.b - Color.red.b) / duration;
        float aStep = (oldColor.a - Color.red.a) / duration;
        Color step = new Color(rStep, gStep, bSTep, aStep);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < duration)
        {
            sprite.color = sprite.color + step;
            yield return null;
        }
        sprite.color = oldColor;
    }
}

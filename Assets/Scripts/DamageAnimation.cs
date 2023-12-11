using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
    private float duration = 250;

    public IEnumerator ChangeColor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color oldColor = sprite.color;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < duration)
        {
            sprite.color = Color.Lerp(oldColor, Color.red, (float)(DateTime.Now - start).TotalMilliseconds / duration);
            yield return new WaitForSeconds(Utils.tickInterval);
        }
        sprite.color = oldColor;
    }
}

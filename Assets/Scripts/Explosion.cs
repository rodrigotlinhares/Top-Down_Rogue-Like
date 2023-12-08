using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Explosion : PlayerAttack
{
    private int fadeDuration = 600;

    private void Awake()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float fadeStep = sr.color.a / fadeDuration;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < fadeDuration)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - fadeStep);
            yield return null;
        }
        Destroy(gameObject);
    }
}

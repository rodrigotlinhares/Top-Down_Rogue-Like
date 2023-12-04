using System;
using System.Collections;
using UnityEngine;

public class Slash : PlayerAttack
{
    [SerializeField] private float duration;

    private void Awake()
    {
        StartCoroutine(Dissipate());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private IEnumerator Dissipate()
    {
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < 200)
            yield return null;
        Destroy(gameObject);
    }
}

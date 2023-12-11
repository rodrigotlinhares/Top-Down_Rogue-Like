using System;
using System.Collections;
using UnityEngine;

public class Stab : PlayerAttack
{
    [SerializeField] private float duration;

    void Start()
    {
        StartCoroutine(Dissipate());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    IEnumerator Dissipate()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}

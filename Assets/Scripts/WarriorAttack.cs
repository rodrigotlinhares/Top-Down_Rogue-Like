using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarriorAttack : MonoBehaviour
{
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
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < 200)
            yield return null;
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour
{
    private bool onCooldown = false;
    private float tickRate = 1f;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(Dissipate());
    }

    private void Update()
    {
        transform.position = transform.parent.position;
        if (!onCooldown && GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            EventSystem.events.OnEnemyLeechDamageTaken(10);
            StartCoroutine(Utils.Cooldown(result => onCooldown = result, tickRate));
        }

    }
        
    private IEnumerator Dissipate()
    {
        DateTime start = DateTime.Now;
        yield return new WaitForSeconds(3f);
        EventSystem.events.BloodPoolDissipate();
        Destroy(gameObject);
    }
}

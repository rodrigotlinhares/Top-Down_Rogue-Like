using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMagePool : MonoBehaviour
{
    private bool offCooldown = true;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(Dissipate());
    }

    private void Update()
    {
        transform.position = transform.parent.position;
        if (offCooldown && GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            EventSystem.events.OnEnemyLeechDamageTaken(10);
            StartCoroutine(Cooldown());
        }

    }

    private IEnumerator Cooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(1);
        offCooldown = true;
    }
        
    private IEnumerator Dissipate()
    {
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < 10000)
            yield return null;
        EventSystem.events.BloodPoolDissipate();
        Destroy(gameObject);
    }
}

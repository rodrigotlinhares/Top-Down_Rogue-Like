using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodPool : PlayerAttack
{
    public float duration;
    private bool tickOnCooldown = false;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(Dissipate());
    }

    private void Update()
    {
        transform.position = transform.parent.position;
        if (!tickOnCooldown && GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy", "Knight")))
        {
            EventSystem.events.OnEnemyLeechDamageTaken(damage);
            StartCoroutine(Utils.Cooldown(result => tickOnCooldown = result, Utils.tickInterval));
        }

    }
        
    private IEnumerator Dissipate()
    {
        yield return new WaitForSeconds(duration);
        EventSystem.events.BloodPoolDissipate();
        Destroy(gameObject);
    }
}

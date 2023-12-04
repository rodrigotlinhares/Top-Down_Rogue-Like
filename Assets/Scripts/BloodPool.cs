using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodPool : MonoBehaviour
{
    [SerializeField] public float cooldown;
    [SerializeField] private float tickInterval;
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
        if (!tickOnCooldown && GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            EventSystem.events.OnEnemyLeechDamageTaken(10);
            StartCoroutine(Utils.Cooldown(result => tickOnCooldown = result, tickInterval));
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

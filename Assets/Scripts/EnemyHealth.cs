using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    protected float currentHealth;
    private bool dead = false;
    private Coroutine routine;

    protected void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Lower(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (!dead)
        {
            dead = true;
            EventSystem.events.EnemyDeath();
            Destroy(gameObject);
        }
    }

    public void Leech(float amount)
    {
        Lower(amount);
        EventSystem.events.OnEnemyLeechDamageTaken(amount);
    }

    public void LeechOverTime(float amount)
    {
        routine = StartCoroutine(LeechRoutine(amount));
    }

    public void StopLeeching()
    {
        StopCoroutine(routine);
    }

    public IEnumerator LeechRoutine(float amount)
    {
        while (true)
        {
            Lower(amount);
            EventSystem.events.OnEnemyLeechDamageTaken(amount);
            yield return new WaitForSeconds(0.05f);
        }
    }
}

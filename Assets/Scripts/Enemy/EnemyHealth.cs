using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    protected float currentHealth;
    private bool dead = false;
    private Slider slider;
    private Coroutine routine;

    protected void Awake()
    {
        currentHealth = maxHealth;
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = currentHealth;
        slider.value = currentHealth;
    }

    public void Lower(float amount)
    {
        currentHealth -= amount;
        slider.value = currentHealth;
        if (currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        if (!dead)
        {
            dead = true;
            EventSystem.events.EnemyDeath();
            GetComponent<EnemyDeath>().Trigger();
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
            yield return new WaitForSeconds(Utils.tickInterval);
        }
    }
}

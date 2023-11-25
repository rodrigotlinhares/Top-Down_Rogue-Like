using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    protected float currentHealth;
    private bool dead = false;

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

    public IEnumerator LowerOverTime(float amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Lower(amount);
            EventSystem.events.OnEnemyLeechDamageTaken(amount);
        }
    }
}

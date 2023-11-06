using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : Health
{
    private bool dead = false;

    public override void Lower(float amount)
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
            EventSystem.events.EnemyLeechDamageTaken(amount);
        }
    }
}

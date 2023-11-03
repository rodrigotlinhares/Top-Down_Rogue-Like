using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void Lower(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            EventSystem.events.OnEnemyDeath();
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

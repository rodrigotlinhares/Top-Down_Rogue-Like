using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void Lower(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
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

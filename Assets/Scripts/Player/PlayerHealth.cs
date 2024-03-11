using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth, regenAmount = 0f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        EventSystem.events.OnEnemyLeechDamageTaken += Raise;
        EventSystem.events.OnBloodMageHealthRegenChosen += IncreaseRegen;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnEnemyLeechDamageTaken -= Raise;
        EventSystem.events.OnBloodMageHealthRegenChosen -= IncreaseRegen;
    }

    public void Lower(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            EventSystem.events.PlayerDeath();
            Destroy(gameObject);
        }
    }

    public void Raise(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        EventSystem.events.OnPlayerHealed(amount);
    }

    private void IncreaseRegen(float amount)
    {
        if (regenAmount == 0)
            StartCoroutine(Regenerate());
        regenAmount += amount;
    }

    private IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Utils.tickInterval);
            Raise(regenAmount);
        }
    }
}

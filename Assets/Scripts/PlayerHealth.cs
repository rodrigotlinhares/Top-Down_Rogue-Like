using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        EventSystem.events.OnEnemyLeechDamageTaken += Raise;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnEnemyLeechDamageTaken -= Raise;
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
}

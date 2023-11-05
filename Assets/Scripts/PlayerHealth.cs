public class PlayerHealth : Health
{
    private void Start()
    {
        EventSystem.events.OnEnemyLeechDamageTaken += Raise;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnEnemyLeechDamageTaken -= Raise;
    }

    public override void Lower(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            EventSystem.events.PlayerDeath();
            Destroy(gameObject);
        }
    }

    private void Raise(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}

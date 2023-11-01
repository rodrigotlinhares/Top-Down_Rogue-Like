using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    private int currentHealth;
    public Action Die;
    public Action<int> TakeDamage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        TakeDamage += Lower;
    }

    private void OnDisable()
    {
        TakeDamage -= Lower;
    }

    private void Lower(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die?.Invoke();
            Destroy(gameObject);
        }
    }
}

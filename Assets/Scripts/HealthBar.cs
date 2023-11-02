using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private Health playerHealth;

    private void OnEnable()
    {
        playerHealth = Object.FindAnyObjectByType<Player>().GetComponent<Health>();
        SetMaxHealth(playerHealth.maxHealth);
        playerHealth.TakeDamage += Lower;
    }

    private void OnDisable()
    {
        playerHealth.TakeDamage -= Lower;
    }

    public void Lower(float health)
    {
        slider.value -= health;
    }

    public void Raise(float health)
    {
        slider.value += health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = slider.maxValue;
    }
}

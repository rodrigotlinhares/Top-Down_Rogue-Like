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
        playerHealth = Object.FindAnyObjectByType<Character>().GetComponent<Health>();
        SetMaxHealth(playerHealth.maxHealth);
    }

    private void Start()
    {
        EventSystem.events.OnPlayerDamageTaken += Lower;
        EventSystem.events.OnPlayerHealed += Raise;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerDamageTaken -= Lower;
        EventSystem.events.OnPlayerHealed -= Raise;
    }

    public void Lower(float amount)
    {
        slider.value -= amount;
    }

    public void Raise(float amount)
    {
        slider.value += amount;
        if (slider.value > playerHealth.maxHealth)
            slider.value = playerHealth.maxHealth;
    }

    public void SetMaxHealth(float amount)
    {
        slider.maxValue = amount;
        slider.value = slider.maxValue;
    }
}

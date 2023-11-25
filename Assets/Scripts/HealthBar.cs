using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : ResourceBar
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        SetMax(playerHealth.maxHealth);
        EventSystem.events.OnPlayerDamageTaken += Lower;
        EventSystem.events.OnPlayerHealed += Raise;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerDamageTaken -= Lower;
        EventSystem.events.OnPlayerHealed -= Raise;
    }
}

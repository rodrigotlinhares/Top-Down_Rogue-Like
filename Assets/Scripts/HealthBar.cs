using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : ResourceBar
{
    private Health playerHealth;

    private void Start()
    {
        playerHealth = Object.FindAnyObjectByType<Character>().GetComponent<Health>();
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

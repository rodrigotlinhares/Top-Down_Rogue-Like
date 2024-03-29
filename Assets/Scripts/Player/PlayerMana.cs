using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] public float maxMana;
    [SerializeField] private float regenAmount;
    [NonSerialized] public float currentMana;

    protected void Awake()
    {
        currentMana = maxMana;
        StartCoroutine(RegenerateMana());
    }

    private void Start()
    {
        EventSystem.events.OnMageManaRegenChosen += IncreaseRegen;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnMageManaRegenChosen -= IncreaseRegen;
    }

    public void Lower(float amount)
    {
        currentMana -= amount;
        EventSystem.events.PlayerManaSpent(amount);
    }

    public void Raise(float amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
            currentMana = maxMana;
        EventSystem.events.PlayerManaRecovered(amount);
    }

    private void IncreaseRegen(float amount)
    {
        regenAmount += amount;
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(Utils.tickInterval);
            Raise(regenAmount);
        }
    }
}

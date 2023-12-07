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

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            Raise(regenAmount);
        }
    }
}

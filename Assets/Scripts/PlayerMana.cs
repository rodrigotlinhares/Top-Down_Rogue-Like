using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] public float maxMana;
    private float currentMana;

    protected void Awake()
    {
        currentMana = maxMana;
    }

    private void Lower(float amount)
    {
        currentMana -= amount;
    }

    private void Raise(float amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
            currentMana = maxMana;
    }
}

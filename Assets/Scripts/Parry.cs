using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.U2D;

public class Parry : MonoBehaviour
{
    [SerializeField] public float cooldown;
    public float leechChance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent<Enemy>() || collision.gameObject.GetComponent<EnemyArrow>()) && leechChance > UnityEngine.Random.value)
            EventSystem.events.PlayerHealed(10f);
    }
}

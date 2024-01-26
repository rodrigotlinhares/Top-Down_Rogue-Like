using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : PlayerAttack
{
    private float leech = 0f;

    private void Start()
    {
        EventSystem.events.OnWarriorAttackSpeedChosen += IncreaseAttackSpeed;
        EventSystem.events.OnWarriorAttackSizeChosen += IncreaseAttackSize;
        EventSystem.events.OnWarriorLeechChosen += IncreaseLeech;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnWarriorAttackSpeedChosen -= IncreaseAttackSpeed;
        EventSystem.events.OnWarriorAttackSizeChosen -= IncreaseAttackSize;
        EventSystem.events.OnWarriorLeechChosen -= IncreaseLeech;
    }

    private void IncreaseAttackSpeed(float amount)
    {
        cooldown *= 1 - amount;
        GetComponent<Animator>().speed *= 1 + amount;
    }

    private void IncreaseAttackSize(float amount)
    {
        transform.localScale = new Vector3(transform.localScale.x * (1 + amount), transform.localScale.y * (1 + amount), 1);
    }

    private void IncreaseLeech(float amount)
    {
        leech += amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            EventSystem.events.EnemyLeechDamageTaken(leech);
    }
}

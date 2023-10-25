using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Warrior : PlayerController
{
    private int attackForce = 350, chargeMultiplier = 4, chargeTime = 250, chargeStunTime = 250, chargeForce = 1000;
    private bool blocking = false, charging = false;
    private WarriorAttack attack;
    private WarriorBlock block, blockClone;

    void Start()
    {
        health = ClassStats.stats[className].health;
        body = GetComponent<Rigidbody2D>();
        attack = Resources.Load<WarriorAttack>("Prefabs/WarriorAttack");
        block = Resources.Load<WarriorBlock>("Prefabs/WarriorBlock");
    }

    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                BeginBlocking();
            if (Input.GetKey(KeyCode.Mouse1) && blocking)
                Block(Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse1) && blocking)
                StopBlocking();
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(Charge());
        }
    }

    private void Attack(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        WarriorAttack clone = Instantiate(attack, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private void BeginBlocking()
    {
        blocking = true;
        blockClone = Instantiate(block, body.transform);
    }

    private void Block(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        blockClone.transform.position = body.position + direction;
        blockClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    private void StopBlocking()
    {
        blocking = false;
        Destroy(blockClone.gameObject);
    }

    IEnumerator Charge()
    {
        inputEnabled = false;
        charging = true;
        body.velocity = new Vector2(body.velocity.x * chargeMultiplier, body.velocity.y * chargeMultiplier);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < chargeTime)
            yield return null;
        inputEnabled = true;
        charging = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy && charging)
        {
            body.velocity = Vector2.zero;
            Vector2 direction = ((Vector2)collision.transform.position - GetComponent<Rigidbody2D>().position).normalized;
            enemy.StartCoroutine(enemy.Stun(chargeStunTime));
            collision.rigidbody.AddForce(direction * chargeForce);
        }
        if (enemy && !charging)
        {
            if (blockClone)
                StopBlocking();
            StartCoroutine(TakeDamage(collision.gameObject));
        }
    }
}

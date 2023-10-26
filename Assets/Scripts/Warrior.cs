using System;
using System.Collections;
using UnityEngine;

public class Warrior : PlayerController
{
    private int attackForce = 350, chargeMultiplier = 4, chargeTime = 250;
    private bool blocking = false;
    private WarriorAttack attack;
    private WarriorBlock block, blockClone;
    private PlayerCollision chargeCollision;

    void Start()
    {
        currentHealth = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        chargeCollision = gameObject.AddComponent<ChargeCollision>();
        chargeCollision.enabled = false;
        attack = Resources.Load<WarriorAttack>("Prefabs/WarriorAttack");
        block = Resources.Load<WarriorBlock>("Prefabs/WarriorBlock");
    }

    void Update()
    {
        if (inputEnabled)
        {
            movement.Move();
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
        chargeCollision.enabled = true;
        body.velocity = new Vector2(body.velocity.x * chargeMultiplier, body.velocity.y * chargeMultiplier);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < chargeTime)
            yield return null;
        inputEnabled = true;
        chargeCollision.enabled = false;
    }
}

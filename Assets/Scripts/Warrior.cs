using System;
using System.Collections;
using UnityEngine;

public class Warrior : Player
{
    [SerializeField] private WarriorAttack attack;
    [SerializeField] private WarriorBlock block;
    private int attackForce = 350, chargeMultiplier = 4, chargeTime = 250;
    private bool blocking = false;
    private WarriorBlock blockClone;

    [NonSerialized] public ChargeCollision chargeCollision;

    new private void Awake()
    {
        base.Awake();
        chargeCollision = gameObject.GetComponent<ChargeCollision>();
    }

    private void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0) && !mainAttackOnCooldown)
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                BeginBlocking();
            if (Input.GetKeyDown(KeyCode.Space) && !utilityOnCooldown)
                StartCoroutine(Charge());
        }
        if (Input.GetKey(KeyCode.Mouse1) && blocking)
            Block(Input.mousePosition);
        if (Input.GetKeyUp(KeyCode.Mouse1) && blocking)
            StopBlocking();
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(Cooldown(result => mainAttackOnCooldown = result, mainAttackCooldown));
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
        StartCoroutine(Cooldown(result => utilityOnCooldown = result, utilityCooldown));
        movement.enabled = false;
        chargeCollision.enabled = true;
        body.velocity = new Vector2(body.velocity.x * chargeMultiplier, body.velocity.y * chargeMultiplier);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < chargeTime)
            yield return null;
        movement.enabled = true;
        chargeCollision.enabled = false;
    }
}

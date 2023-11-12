using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Character
{
    [SerializeField] private WarlockAttack attack;
    [SerializeField] private WarlockKnockback knock;
    private int attackForce = 10, knockbackForce = 200;

    private new void Awake()
    {
        base.Awake();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0) && !mainAttackOnCooldown)
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1) && !secAttackOnCooldown)
                ExplodeDots();
            if (Input.GetKeyDown(KeyCode.Space) && !utilityOnCooldown)
                ShootKnockback(Input.mousePosition);
        }
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(Cooldown(result => mainAttackOnCooldown = result, mainAttackCooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        WarlockAttack clone = Instantiate(attack, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private void ExplodeDots()
    {
        StartCoroutine(Cooldown(result => secAttackOnCooldown = result, secAttackCooldown));
        EventSystem.events.WarlockExplodeDots();
    }

    private void ShootKnockback(Vector3 target)
    {
        StartCoroutine(Cooldown(result => utilityOnCooldown = result, utilityCooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        WarlockKnockback clone = Instantiate(knock, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce);
    }
}

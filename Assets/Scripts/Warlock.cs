using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Player
{
    [SerializeField] private WarlockAttack attack;
    [SerializeField] private WarlockKnockback knock;
    private int attackForce = 10, knockbackForce = 200;

    private void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                ExplodeDots();
            if (Input.GetKeyDown(KeyCode.Space))
                ShootKnockback(Input.mousePosition);
        }
    }

    private void Attack(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        WarlockAttack clone = Instantiate(attack, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private void ExplodeDots()
    {
        EventSystem.events.WarlockExplodeDots();
    }

    private void ShootKnockback(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        WarlockKnockback clone = Instantiate(knock, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce);
    }
}

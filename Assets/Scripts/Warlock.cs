using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Player
{
    [SerializeField]
    private WarlockAttack attack;
    private int attackForce = 10;

    private void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                ExplodeDots();
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
}

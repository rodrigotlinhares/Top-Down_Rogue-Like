using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static Unity.Collections.AllocatorManager;

public class Rogue : MonoBehaviour, PlayerController.PlayerClass
{
    public PlayerController Controller { get; set; }
    public int Health { get; set; }

    private int attackForce = 350, attackCooldown = 300, attackSide = 1;
    private bool attackOnCooldown = false;
    private string className = "Rogue";
    private Rigidbody2D body;
    private RogueAttack attack;
    private RogueParry parry;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Health = ClassStats.stats[className].health;
        attack = Resources.Load<RogueAttack>("Prefabs/RogueAttack");
        parry = Resources.Load<RogueParry>("Prefabs/RogueParry");

    }
    void Update()
    {
        if (Controller.InputEnabled)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !attackOnCooldown)
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                Parry(Input.mousePosition);
        }
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(AttackCooldown());
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - Controller.body.position).normalized;
        RogueAttack clone = Instantiate(attack, Controller.body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.transform.position += (Vector3)(Vector2.Perpendicular(direction) * 0.2f * attackSide);
        attackSide *= -1;
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private IEnumerator AttackCooldown()
    {
        attackOnCooldown = true;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < attackCooldown)
            yield return null;
        attackOnCooldown = false;
    }

    private void Parry(Vector3 target)
    {
        RogueParry parryClone = Instantiate(parry, Controller.body.transform);
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - Controller.body.position).normalized;
        parryClone.transform.position = Controller.body.position + direction;
        parryClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }
}

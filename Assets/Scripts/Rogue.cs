using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static Unity.Collections.AllocatorManager;

public class Rogue : PlayerController
{
    private int attackForce = 350, attackCooldown = 300, parryCooldown = 400, attackSide = 1, parryFadeDuration = 300, dashMultiplier = 4, dashTime = 250;
    private bool attackOnCooldown = false, parryOnCooldown = false;
    private RogueAttack attack;
    private RogueParry parry, parryClone;

    void Start()
    {
        health = ClassStats.stats[className].health;
        body = GetComponent<Rigidbody2D>();
        attack = Resources.Load<RogueAttack>("Prefabs/RogueAttack");
        parry = Resources.Load<RogueParry>("Prefabs/RogueParry");

    }
    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !attackOnCooldown)
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1) && !parryOnCooldown)
                Parry(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(Dash());
        }
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(AttackCooldown());
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        RogueAttack clone = Instantiate(attack, body.transform);
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
        StartCoroutine(ParryCooldown());
        parryClone = Instantiate(parry, body.transform);
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        parryClone.transform.position = body.position + direction;
        parryClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        StartCoroutine(Fade(direction));
    }
    private IEnumerator ParryCooldown()
    {
        parryOnCooldown = true;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < parryCooldown)
            yield return null;
        parryOnCooldown = false;
    }

    private IEnumerator Fade(Vector2 direction)
    {
        SpriteShapeRenderer ssr = parryClone.GetComponent<SpriteShapeRenderer>();
        float stepSize = ssr.color.a / parryFadeDuration;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < parryFadeDuration)
        {
            ssr.color = new Color(ssr.color.r, ssr.color.g, ssr.color.b, ssr.color.a - stepSize);
            parryClone.transform.position = body.position + direction;
            yield return null;
        }
        Destroy(parryClone.gameObject);
    }

    IEnumerator Dash()
    {
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Enemy");
        inputEnabled = false;
        body.velocity = new Vector2(body.velocity.x * dashMultiplier, body.velocity.y * dashMultiplier);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < dashTime)
            yield return null;
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask();
        inputEnabled = true;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            StartCoroutine(TakeDamage(collision.gameObject));
    }
}
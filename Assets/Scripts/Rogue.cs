using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class Rogue : Character
{
    [SerializeField]
    private RogueAttack attack;

    [SerializeField]
    private RogueParry parry;

    private int attackForce = 350, attackSide = 1, parryFadeDuration = 300, dashMultiplier = 4;
    private float dashTime = 0.25f;
    private RogueParry parryClone;

    private new void Awake()
    {
        base.Awake();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKey(KeyCode.Mouse0) && !mainAttackOnCooldown)
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1) && !secAttackOnCooldown)
                Parry(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Space) && !utilityOnCooldown)
                StartCoroutine(Dash());
        }
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(Cooldown(result => mainAttackOnCooldown = result, mainAttackCooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        RogueAttack clone = Instantiate(attack, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.transform.position += (Vector3)(Vector2.Perpendicular(direction) * 0.2f * attackSide);
        attackSide *= -1;
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private void Parry(Vector3 target)
    {
        StartCoroutine(Cooldown(result => secAttackOnCooldown = result, secAttackCooldown));
        parryClone = Instantiate(parry, body.transform);
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        parryClone.transform.position = body.position + direction;
        parryClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        StartCoroutine(Fade(direction));
    }

    private IEnumerator Fade(Vector2 direction)
    {
        SpriteShapeRenderer ssr = parryClone.GetComponent<SpriteShapeRenderer>();
        float fadeStep = ssr.color.a / parryFadeDuration;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < parryFadeDuration)
        {
            ssr.color = new Color(ssr.color.r, ssr.color.g, ssr.color.b, ssr.color.a - fadeStep);
            parryClone.transform.position = body.position + direction;
            yield return null;
        }
        Destroy(parryClone.gameObject);
    }

    IEnumerator Dash()
    {
        StartCoroutine(Cooldown(result => utilityOnCooldown = result, utilityCooldown));
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Enemy");
        movement.enabled = false;
        body.velocity = new Vector2(body.velocity.x * dashMultiplier, body.velocity.y * dashMultiplier);
        DateTime start = DateTime.Now;
        yield return new WaitForSeconds(dashTime);
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask();
        movement.enabled = true;
    }
}

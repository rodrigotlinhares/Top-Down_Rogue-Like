using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class Rogue : Character
{
    [SerializeField] private Stab stab;
    [SerializeField] private Parry parry;
    [SerializeField] private float dashCooldown;
    private bool stabOnCooldown = false, parryOnCooldown = false, dashOnCooldown = false;
    private int stabForce = 600, stabSide = 1, dashMultiplier = 16;
    private float dashTime = 0.2f, parryFadeDuration = 300f;
    private Rigidbody2D body;
    private Parry parryClone;
    private PlayerMovement movement;

    private void Awake()
    {
        stab.cooldown = 0.3f;
        stab.explosionChance = 0f;
        parry.leechChance = 0f;
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        EventSystem.events.OnRogueAttackSpeedChosen += IncreaseAttackSpeed;
        EventSystem.events.OnRogueAttackExplosionChosen += IncreaseExplosionChance;
        EventSystem.events.OnRogueDashCooldownChosen += LowerDashCooldown;
        EventSystem.events.OnRogueParryDurationChosen += IncreaseParryDuration;
        EventSystem.events.OnRogueParryLeechChosen += IncreaseParryLeechChance;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnRogueAttackSpeedChosen -= IncreaseAttackSpeed;
        EventSystem.events.OnRogueAttackExplosionChosen -= IncreaseExplosionChance;
        EventSystem.events.OnRogueDashCooldownChosen -= LowerDashCooldown;
        EventSystem.events.OnRogueParryDurationChosen -= IncreaseParryDuration;
        EventSystem.events.OnRogueParryLeechChosen -= IncreaseParryLeechChance;
    }

    void Update()
    {
        if (movement.enabled)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !stabOnCooldown)
                Stab(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1) && !parryOnCooldown)
                Parry(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Space) && !dashOnCooldown)
                StartCoroutine(Dash());
        }
    }

    private void Stab(Vector3 target)
    {
        StartCoroutine(Utils.Cooldown(result => stabOnCooldown = result, stab.cooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        Stab clone = Instantiate(stab, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(new Vector3(-4.7f, -8.5f, 0), direction);
        clone.transform.position += (Vector3)(Vector2.Perpendicular(direction) * 0.2f * stabSide);
        stabSide *= -1;
        clone.GetComponent<Rigidbody2D>().AddForce(direction * stabForce);
    }

    private void Parry(Vector3 target)
    {
        StartCoroutine(Utils.Cooldown(result => parryOnCooldown = result, parry.cooldown));
        parryClone = Instantiate(parry, body.transform);
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        parryClone.transform.position = body.position + direction;
        parryClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        StartCoroutine(FadeParry(direction));
    }

    private IEnumerator FadeParry(Vector2 direction)
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
        StartCoroutine(Utils.Cooldown(result => dashOnCooldown = result, dashCooldown));
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Enemy", "Enemy Projectile");
        movement.enabled = false;
        body.velocity = new Vector2(movement.CurrentInput().x * dashMultiplier, movement.CurrentInput().y * dashMultiplier);
        DateTime start = DateTime.Now;
        yield return new WaitForSeconds(dashTime);
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask();
        movement.enabled = true;
    }

    private void IncreaseAttackSpeed(float amount)
    {
        stab.cooldown *= 1 - amount;
    }

    private void IncreaseExplosionChance(float amount)
    {
        stab.explosionChance += amount;
    }

    private void LowerDashCooldown(float amount)
    {
        dashCooldown *= 1 - amount;
    }

    private void IncreaseParryDuration(float amount)
    {
        parryFadeDuration *= 1 + amount;
    }

    private void IncreaseParryLeechChance(float amount)
    {
        parry.leechChance += amount;
    }
}

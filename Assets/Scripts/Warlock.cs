using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Character
{
    [SerializeField] private Corruption corruption;
    [SerializeField] private Demon demon;
    [SerializeField] private Explosion explosion;
    private bool corruptionOnCooldown = false, explosionOnCooldown = false, demonOnCooldown = false;
    private int attackForce = 5, knockbackForce = 200;
    private Rigidbody2D body;
    private PlayerMovement movement;

    private void Awake()
    {
        corruption.damageOverTime = 0.15f;
        corruption.cooldown = 0.25f;
        demon.stunForce = 500;
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        EventSystem.events.OnWarlockCorruptionDamageChosen += IncreaseCorruptionDamage;
        EventSystem.events.OnWarlockCorruptionCooldownChosen += LowerCorruptionCooldown;
        EventSystem.events.OnWarlockExplosionCooldownChosen += LowerExplosionCooldown;
        EventSystem.events.OnWarlockDemonSizeChosen += IncreaseDemonSize;
        EventSystem.events.OnWarlockDemonKnockbackChosen += IncreaseDemonKnockback;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnWarlockCorruptionDamageChosen -= IncreaseCorruptionDamage;
        EventSystem.events.OnWarlockCorruptionCooldownChosen -= LowerCorruptionCooldown;
        EventSystem.events.OnWarlockExplosionCooldownChosen -= LowerExplosionCooldown;
        EventSystem.events.OnWarlockDemonSizeChosen -= IncreaseDemonSize;
        EventSystem.events.OnWarlockDemonKnockbackChosen -= IncreaseDemonKnockback;
    }

    private void Update()
    {
        if (movement.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !corruptionOnCooldown)
                Corrupt(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1) && !explosionOnCooldown)
                ExplodeCorruption();
            if (Input.GetKeyDown(KeyCode.Space) && !demonOnCooldown)
                SummonDemon(Input.mousePosition);
        }
    }

    private void Corrupt(Vector3 target)
    {
        StartCoroutine(Utils.Cooldown(result => corruptionOnCooldown = result, corruption.cooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        Corruption clone = Instantiate(corruption, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private void ExplodeCorruption()
    {
        StartCoroutine(Utils.Cooldown(result => explosionOnCooldown = result, explosion.cooldown));
        EventSystem.events.WarlockExplodeDots();
    }

    private void SummonDemon(Vector3 target)
    {
        StartCoroutine(Utils.Cooldown(result => demonOnCooldown = result, demon.cooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        Demon clone = Instantiate(demon, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce);
    }

    private void IncreaseCorruptionDamage(float amount)
    {
        corruption.damageOverTime *= 1 + amount;
    }

    private void LowerCorruptionCooldown(float amount)
    {
        corruption.cooldown *= 1 - amount;
    }

    private void LowerExplosionCooldown(float amount)
    {
        explosion.cooldown *= 1 - amount;
    }

    private void IncreaseDemonSize(float amount)
    {
        Vector3 scale = demon.transform.localScale;
        demon.transform.localScale = new Vector3(scale.x * (1 + amount), scale.y * (1 + amount), 1);
    }

    private void IncreaseDemonKnockback(float amount)
    {
        demon.stunForce *= 1 - amount;
    }
}

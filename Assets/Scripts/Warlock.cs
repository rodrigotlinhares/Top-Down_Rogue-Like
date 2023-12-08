using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Character
{
    [SerializeField] private Corruption corruption;
    [SerializeField] private Demon demon;
    [SerializeField] private Explosion explosion;
    private bool corruptionOnCooldown = false, explosionOnCooldown = false, demonOnCooldown = false;
    private int attackForce = 10, knockbackForce = 200;
    private Rigidbody2D body;
    private PlayerMovement movement;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
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
}

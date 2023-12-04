using System;
using System.Collections;
using UnityEngine;

public class Warrior : Character
{
    [SerializeField] private Slash slash;
    [SerializeField] private Block block;
    [SerializeField] private float chargeCooldown;
    private bool slashOnCooldown = false, chargeOnCooldown = false;
    private int attackForce = 600, chargeMultiplier = 4, chargeTime = 250;
    private bool blocking = false;
    private Rigidbody2D body;
    private Block blockClone;
    private PlayerMovement movement;

    [NonSerialized] public ChargeCollision chargeCollision;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        chargeCollision = gameObject.GetComponent<ChargeCollision>();
    }

    private void Update()
    {
        if (movement.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !slashOnCooldown)
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                BeginBlocking();
            if (Input.GetKeyDown(KeyCode.Space) && !chargeOnCooldown)
                StartCoroutine(Charge());
        }
        if (Input.GetKey(KeyCode.Mouse1) && blocking)
            Block(Input.mousePosition);
        if (Input.GetKeyUp(KeyCode.Mouse1) && blocking)
            StopBlocking();
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(Utils.Cooldown(result => slashOnCooldown = result, slash.cooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        Slash clone = Instantiate(slash, body.transform);
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
        StartCoroutine(Utils.Cooldown(result => chargeOnCooldown = result, chargeCooldown));
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

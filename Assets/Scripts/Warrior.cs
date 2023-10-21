using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Warrior : MonoBehaviour, PlayerController.PlayerClass
{
    public PlayerController Controller { get; set; }
    public int Health { get; set; }

    private int chargeMultiplier = 4, chargeTime = 250, chargeStunTime = 250, chargeForce = 1000;
    private bool blocking = false, charging = false;
    private string className = "Warrior";
    private Rigidbody2D body;
    private WarriorAttack attack;
    private WarriorBlock block, blockClone;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Health = ClassStats.stats[className].health;
        attack = Resources.Load<WarriorAttack>("Prefabs/WarriorAttack");
        block = Resources.Load<WarriorBlock>("Prefabs/WarriorBlock");
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.InputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                BeginBlocking();
            if (Input.GetKey(KeyCode.Mouse1) && blocking)
                Block(Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse1))
                StopBlocking();
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(Charge());
        }
    }

    private void Attack(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - Controller.body.position).normalized;
        WarriorAttack clone = Instantiate(attack, Controller.body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * 350);
    }

    private void BeginBlocking()
    {
        blocking = true;
        blockClone = Instantiate(block, Controller.body.transform);
    }

    private void Block(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - Controller.body.position).normalized;
        blockClone.transform.position = Controller.body.position + direction;
        blockClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    private void StopBlocking()
    {
        blocking = false;
        Destroy(blockClone.gameObject);
    }

    IEnumerator Charge()
    {
        Controller.InputEnabled = false;
        charging = true;
        body.velocity = new Vector2(body.velocity.x * chargeMultiplier, body.velocity.y * chargeMultiplier);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < chargeTime)
            yield return null;
        Controller.InputEnabled = true;
        charging = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy && charging)
        {
            body.velocity = Vector2.zero;
            Vector2 direction = ((Vector2)collision.transform.position - GetComponent<Rigidbody2D>().position).normalized;
            enemy.StartCoroutine(enemy.Stun(chargeStunTime));
            collision.rigidbody.AddForce(direction * chargeForce);
        }
        if (enemy && !charging)
        {
            StartCoroutine(Controller.TakeDamage(collision.gameObject));
        }
    }
}

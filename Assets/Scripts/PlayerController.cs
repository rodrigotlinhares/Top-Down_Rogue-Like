using System;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public Projectile projectile;
    public WarriorAttack warriorAttack;
    public static Action OnDeath;
    public static string className;

    private Vector2 velocity;
    private ClassStats.Stats baseStats;
    public bool inputEnabled = true;
    private int health, dashMultiplier = 4, dashTime = 250, iFrames = 250, shootForce = 1000, knockbackForce = 500;
    private float movementSpeed = 4f;

    public interface PlayerClass
    {
        int Health { get; set; }
        PlayerController Controller { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerClass playerClass;
        if (className == "Warrior")
        {
            playerClass = gameObject.AddComponent<Warrior>();
            playerClass.Controller = this;
        }

        baseStats = ClassStats.stats[className];
        health = baseStats.health;
        GetComponent<SpriteRenderer>().color = baseStats.color;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
                velocity.x = -movementSpeed;
            if (Input.GetKey(KeyCode.D))
                velocity.x = movementSpeed;
            if (Input.GetKey(KeyCode.W))
                velocity.y = movementSpeed;
            if (Input.GetKey(KeyCode.S))
                velocity.y = -movementSpeed;
            body.velocity = velocity;
            if (Input.GetKey(KeyCode.Space))
                Dash();
        }
    }

    private void Shoot(Vector3 target = default(Vector3))
    {
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(target) - body.position;
        direction.Normalize();
        Projectile clone = Instantiate(projectile, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * shootForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            StartCoroutine(TakeDamage(collision.gameObject));
            if (health < 1)
            {
                OnDeath();
                Destroy(gameObject);
            }
        }
    }

    private void Dash(Vector3 target = default(Vector3))
    {
        StartCoroutine(DDash());
    }

    IEnumerator DDash()
    {
        inputEnabled = false;
        DateTime start = DateTime.Now;
        velocity.x *= dashMultiplier;
        velocity.y *= dashMultiplier;
        body.velocity = velocity;
        while ((DateTime.Now - start).Milliseconds < dashTime)
            yield return null;
        inputEnabled = true;
    }

    IEnumerator TakeDamage(GameObject other)
    {
        inputEnabled = false;
        health--;
        Vector2 direction = (body.position - other.GetComponent<Rigidbody2D>().position).normalized;
        body.AddForce(direction * knockbackForce);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).Milliseconds < iFrames)
            yield return null;
        inputEnabled = true;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile projectile;
    public delegate void PlayerDeath();
    public static event PlayerDeath OnDeath;
    public static string className;

    private Rigidbody2D body;
    private Vector2 velocity;
    private ClassStats.Stats baseStats;
    private bool inputEnabled = true;
    private int health, dashMultiplier = 4, dashTime = 250, iFrames = 250, shootForce = 1000, knockbackForce = 500;
    private float movementSpeed = 4f;
    private PlayerClass playerClass;

    public struct PlayerClass
    {
        public int health;
        public Action<Vector3> MainSkill, SecondarySkill, UtilitySkill;

        public PlayerClass(int h, Action<Vector3> ms, Action<Vector3> ss, Action<Vector3> us)
        {
            health = h;
            MainSkill = ms;
            SecondarySkill = ss;
            UtilitySkill = us;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(className == "Warrior")
            playerClass = new PlayerClass(ClassStats.stats[className].health, Shoot, Shoot, Dash);
        if (className == "Mage")
            playerClass = new PlayerClass(ClassStats.stats[className].health, Shoot, Shoot, Dash);
        if (className == "Rogue")
            playerClass = new PlayerClass(ClassStats.stats[className].health, Shoot, Shoot, Dash);
        if (className == "BloodMage")
            playerClass = new PlayerClass(ClassStats.stats[className].health, Shoot, Shoot, Dash);
        if (className == "Warlock")
            playerClass = new PlayerClass(ClassStats.stats[className].health, Shoot, Shoot, Dash);

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
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Shoot(Input.mousePosition);
        }
    }

    private void Shoot(Vector3 target = default(Vector3))
    {
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(target) - body.position;
        direction.Normalize();
        Projectile clone = Instantiate(projectile, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction*shootForce);
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
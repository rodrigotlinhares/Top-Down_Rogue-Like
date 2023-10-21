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
    public bool InputEnabled { get; set; }

    private Vector2 velocity;
    private PlayerClass playerClass;
    private int iFrames = 250, shootForce = 1000, knockbackForce = 500;
    private float movementSpeed = 4f;

    public interface PlayerClass
    {
        int Health { get; set; }
        PlayerController Controller { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        InputEnabled = true;
        if (className == "Warrior")
        {
            playerClass = gameObject.AddComponent<Warrior>();
            playerClass.Controller = this;
        }

        GetComponent<SpriteRenderer>().color = ClassStats.stats[className].color;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputEnabled)
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
        }
    }

    private void Shoot(Vector3 target = default(Vector3))
    {
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(target) - body.position;
        direction.Normalize();
        Projectile clone = Instantiate(projectile, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * shootForce);
    }

    public IEnumerator TakeDamage(GameObject other)
    {
        InputEnabled = false;
        playerClass.Health--;
        if (playerClass.Health < 1)
        {
            OnDeath();
            Destroy(gameObject);
        }
        Vector2 direction = (body.position - other.GetComponent<Rigidbody2D>().position).normalized;
        body.AddForce(direction * knockbackForce);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < iFrames)
            yield return null;
        InputEnabled = true;
    }
}
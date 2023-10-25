using System;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Vector2 velocity;
    protected static bool inputEnabled = true;
    protected private int iFrames = 250, knockbackForce = 500, health;
    protected float movementSpeed = 4f;

    public static string className;
    public static Action OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        if (className == "Warrior")
            gameObject.AddComponent<Warrior>();
        else if (className == "Mage")
            gameObject.AddComponent<Mage>();
        else if (className == "Rogue")
            gameObject.AddComponent<Rogue>();

        body = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = ClassStats.stats[className].color;
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
        }
    }

    protected IEnumerator TakeDamage(GameObject other)
    {
        inputEnabled = false;
        health--;
        if (health < 1)
        {
            OnDeath();
            Destroy(gameObject);
        }
        Vector2 direction = (body.position - other.GetComponent<Rigidbody2D>().position).normalized;
        body.AddForce(direction * knockbackForce);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < iFrames)
            yield return null;
        inputEnabled = true;
    }
}
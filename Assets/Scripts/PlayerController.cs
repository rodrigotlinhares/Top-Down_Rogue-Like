using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Health currentHealth;
    protected PlayerCollision playerCollision;
    protected PlayerMovement movement;
    protected Stun stun;
    protected Rigidbody2D body;
    protected private int iFrames = 250, knockbackForce = 500;

    public static bool inputEnabled = true;
    public static string className;

    // Start is called before the first frame update
    void Start()
    {
        if (className == "Warrior")
            gameObject.AddComponent<Warrior>();
        else if (className == "Mage")
            gameObject.AddComponent<Mage>();
        else if (className == "Rogue")
            gameObject.AddComponent<Rogue>();
        else if (className == "Blood Mage")
            gameObject.AddComponent<BloodMage>();

        body = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = ClassStats.stats[className].color;
    }

    public static IEnumerator DisableInput(int duration)
    {
        inputEnabled = false;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < duration)
            yield return null;
        inputEnabled = true;
    }
}
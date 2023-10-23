using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Mage : MonoBehaviour, PlayerController.PlayerClass
{
    public PlayerController Controller { get; set; }
    public int Health { get; set; }

    private int attackForce = 1000, blinkDistance = 3;
    private string className = "Mage";
    private Rigidbody2D body;
    private MageAttack attack;
    private MageShield shield, shieldClone;
    private Bounds blinkBounds;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Health = ClassStats.stats[className].health;
        attack = Resources.Load<MageAttack>("Prefabs/MageAttack");
        shield = Resources.Load<MageShield>("Prefabs/MageShield");
        blinkBounds = GameObject.Find("BlinkBounds").GetComponent<SpriteRenderer>().bounds;
    }
    void Update()
    {
        if (Controller.InputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                BeginShielding();
            if (Input.GetKey(KeyCode.Mouse1))
                Shield();
            if (Input.GetKeyUp(KeyCode.Mouse1))
                StopShielding();
            if (Input.GetKeyDown(KeyCode.Space))
                Blink();
        }
    }

    private void Attack(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        MageAttack clone = Instantiate(attack, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }
    private void BeginShielding()
    {
        shieldClone = Instantiate(shield, Controller.body.transform);
    }

    private void Shield()
    {
        shieldClone.transform.position = body.transform.position;
    }

    private void StopShielding()
    {
        Destroy(shieldClone.gameObject);
    }

    private void Blink()
    {
        body.position = blinkBounds.ClosestPoint(body.position + body.velocity.normalized * blinkDistance);
    }
}

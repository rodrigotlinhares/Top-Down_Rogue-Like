using UnityEngine;

public class Mage : PlayerController
{
    private int attackForce = 1000, blinkDistance = 3;
    private MageAttack attack;
    private MageShield shield, shieldClone;
    private Bounds blinkBounds;

    private void Start()
    {
        currentHealth = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        attack = Resources.Load<MageAttack>("Prefabs/MageAttack");
        shield = Resources.Load<MageShield>("Prefabs/MageShield");
        blinkBounds = GameObject.Find("BlinkBounds").GetComponent<SpriteRenderer>().bounds;
    }
    void Update()
    {
        if (inputEnabled)
        {
            movement.Move();
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
        shieldClone = Instantiate(shield, body.transform);
    }

    private void Shield()
    {
        if (shieldClone)
            shieldClone.transform.position = body.transform.position;
    }

    private void StopShielding()
    {
        if (shieldClone)
            Destroy(shieldClone.gameObject);
    }

    private void Blink()
    {
        body.position = blinkBounds.ClosestPoint(body.position + body.velocity.normalized * blinkDistance);
    }
}

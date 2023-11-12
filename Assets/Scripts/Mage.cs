using UnityEngine;

public class Mage : Character
{
    [SerializeField]
    private MageAttack attack;

    [SerializeField]
    private MageShield shield;

    private int attackForce = 1000, blinkDistance = 3;
    private MageShield shieldClone;
    private Bounds blinkBounds;

    new private void Awake()
    {
        base.Awake();
        movement = GetComponent<PlayerMovement>();
        blinkBounds = GameObject.Find("SpawnBounds").GetComponent<SpriteRenderer>().bounds;
    }

    void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0) && !mainAttackOnCooldown)
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1) && !secAttackOnCooldown)
                BeginShielding();
            if (Input.GetKeyUp(KeyCode.Mouse1))
                StopShielding();
            if (Input.GetKeyDown(KeyCode.Space) && !utilityOnCooldown)
                Blink();
        }
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(Cooldown(result => mainAttackOnCooldown = result, mainAttackCooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        MageAttack clone = Instantiate(attack, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private void BeginShielding()
    {
        StartCoroutine(Cooldown(result => secAttackOnCooldown = result, secAttackCooldown));
        shieldClone = Instantiate(shield, body.transform);
    }

    private void StopShielding()
    {
        if (shieldClone)
            Destroy(shieldClone.gameObject);
    }

    private void Blink()
    {
        StartCoroutine(Cooldown(result => utilityOnCooldown = result, utilityCooldown));
        body.position = blinkBounds.ClosestPoint(body.position + body.velocity.normalized * blinkDistance);
    }
}

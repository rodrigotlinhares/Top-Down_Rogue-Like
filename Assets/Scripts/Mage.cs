using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mage : Character
{
    [SerializeField] private MageAttack attack;
    [SerializeField] private MageBigAttack bigAttack;
    [SerializeField] private MageShield shield;

    private int attackForce = 1000, blinkDistance = 3, bigAttackCharge = 0, bigAttackMaxCharge = 200;
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
            if (Input.GetKeyDown(KeyCode.Mouse1))
                StartCoroutine(ChargeBigAttack());
            if (Input.GetKeyDown(KeyCode.Space) && !utilityOnCooldown)
                BeginShielding();
            if (Input.GetKeyUp(KeyCode.Space))
                StopShielding();
        }
    }

    private void Attack(Vector3 target)
    {
        StartCoroutine(Cooldown(result => mainAttackOnCooldown = result, mainAttackCooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        MageAttack clone = Instantiate(attack, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    private void BigAttack(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        MageBigAttack clone = Instantiate(bigAttack, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
        EventSystem.events.PlayerManaSpent(10);
    }

    private IEnumerator ChargeBigAttack()
    {
        bigAttackCharge = 0;
        while (Input.GetKey(KeyCode.Mouse1))
        {
            bigAttackCharge++;
            if (bigAttackCharge >= bigAttackMaxCharge)
            {
                BigAttack(Input.mousePosition);
                break;
            }
            yield return null;
        }
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

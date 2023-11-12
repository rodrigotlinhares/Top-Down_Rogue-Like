using UnityEngine;

public class BloodMage : Character
{
    [SerializeField] private BloodMageAttack attack;
    [SerializeField] private BloodMageProjectile projectile;
    [SerializeField] private BloodMagePool bloodPool;
    private BloodMageAttack attackClone;
    private int projectileForce = 250;
    private bool attacking = false;

    private new void Awake()
    {
        base.Awake();
        health = GetComponent<Health>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        EventSystem.events.OnBloodPoolDissipate += Solidify;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnBloodPoolDissipate -= Solidify;
    }

    void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                BeginBeam();
            if (Input.GetKey(KeyCode.Mouse0) && attacking)
                Beam(Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse0) && attacking)
                StopBeam();
            if (Input.GetKeyDown(KeyCode.Mouse1) && !secAttackOnCooldown)
                Shoot(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Space) && !utilityOnCooldown)
                Liquefy();
        }
    }
    private void BeginBeam()
    {
        attacking = true;
        attackClone = Instantiate(attack, body.transform);
    }

    private void Beam(Vector3 target)
    {
        if (attackClone)
            attackClone.Resize(body.position, (Vector2)Camera.main.ScreenToWorldPoint(target));
    }

    private void StopBeam()
    {
        attacking = false;
        Destroy(attackClone.gameObject);
    }
    private void Shoot(Vector3 target)
    {
        StartCoroutine(Cooldown(result => secAttackOnCooldown = result, secAttackCooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        BloodMageProjectile clone = Instantiate(projectile, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);
        health.Lower(10);
        EventSystem.events.PlayerDamageTaken(10);
    }

    private void Liquefy()
    {
        StartCoroutine(Cooldown(result => utilityOnCooldown = result, utilityCooldown));
        GetComponent<SpriteRenderer>().enabled = false;
        Physics2D.IgnoreLayerCollision(6, 8);
        Instantiate(bloodPool, body.transform);
    }

    private void Solidify()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
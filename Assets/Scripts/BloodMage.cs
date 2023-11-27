using UnityEngine;

public class BloodMage : Character
{
    [SerializeField] private LifeDrain lifeDrain;
    [SerializeField] private BloodOrb bloodOrb;
    [SerializeField] private BloodPool bloodPool;
    [SerializeField] int orbHealthCost;
    [SerializeField] private float poolCooldown;
    private bool poolOnCooldown = false;
    private Rigidbody2D body;
    private PlayerHealth health;
    private PlayerMovement movement;
    private LifeDrain attackClone;
    private int projectileForce = 250;
    private bool attacking = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
                BeginDrain();
            if (Input.GetKey(KeyCode.Mouse0) && attacking)
                Drain(Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse0) && attacking)
                StopDrain();
            if (Input.GetKeyDown(KeyCode.Mouse1))
                LaunchOrb(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Space) && !poolOnCooldown)
                Liquefy();
        }
    }
    private void BeginDrain()
    {
        attacking = true;
        attackClone = Instantiate(lifeDrain, body.transform);
    }

    private void Drain(Vector3 target)
    {
        if (attackClone)
            attackClone.Resize(body.position, (Vector2)Camera.main.ScreenToWorldPoint(target));
    }

    private void StopDrain()
    {
        attacking = false;
        Destroy(attackClone.gameObject);
    }
    private void LaunchOrb(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        BloodOrb clone = Instantiate(bloodOrb, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);
        health.Lower(orbHealthCost);
        EventSystem.events.PlayerDamageTaken(orbHealthCost);
    }

    private void Liquefy()
    {
        StartCoroutine(Cooldown(result => poolOnCooldown = result, poolCooldown));
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
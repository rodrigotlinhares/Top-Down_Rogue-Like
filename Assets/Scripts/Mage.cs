using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mage : Character
{
    [SerializeField] private ArcaneBolt arcaneBolt;
    [SerializeField] private ArcaneBlast arcanBlast;
    [SerializeField] private ArcaneShield arcaneShield;
    [SerializeField] private float boltCooldown;
    [SerializeField] private int blastManaCost;
    private bool boltOnCooldown = false;
    private int boltForce = 1000, blastCharge = 0, blastMaxCharge = 200;
    private Rigidbody2D body;
    private ArcaneShield shieldClone;
    private PlayerMovement movement;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        Resources.FindObjectsOfTypeAll<ManaBar>()[0].gameObject.SetActive(true);
    }

    void Update()
    {
        if (movement.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !boltOnCooldown)
                LaunchArcaneBolt(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                StartCoroutine(ChargeArcaneBlast());
            if (Input.GetKeyDown(KeyCode.Space))
                BeginShielding();
            if (Input.GetKeyUp(KeyCode.Space))
                StopShielding();
        }
    }

    private void LaunchArcaneBolt(Vector3 target)
    {
        StartCoroutine(Utils.Cooldown(result => boltOnCooldown = result, boltCooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        ArcaneBolt clone = Instantiate(arcaneBolt, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * boltForce);
    }

    private void LaunchArcaneBlast(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        ArcaneBlast clone = Instantiate(arcanBlast, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * boltForce);
        EventSystem.events.PlayerManaSpent(blastManaCost);
    }

    private IEnumerator ChargeArcaneBlast()
    {
        blastCharge = 0;
        while (Input.GetKey(KeyCode.Mouse1))
        {
            blastCharge++;
            if (blastCharge >= blastMaxCharge)
            {
                LaunchArcaneBlast(Input.mousePosition);
                break;
            }
            yield return null;
        }
    }

    private void BeginShielding()
    {
        shieldClone = Instantiate(arcaneShield, body.transform);
    }

    private void StopShielding()
    {
        if (shieldClone)
            Destroy(shieldClone.gameObject);
    }
}

using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mage : Character
{
    [SerializeField] private ArcaneBolt arcaneBolt;
    [SerializeField] private ArcaneBlast arcaneBlast;
    [SerializeField] private ArcaneShield arcaneShield;
    private bool boltOnCooldown = false, shieldOnCooldown = false;
    private int boltQuantity = 1, boltForce = 300;
    private Rigidbody2D body;
    private ArcaneShield shieldClone;
    private PlayerMovement movement;
    private PlayerMana mana;

    private void Awake()
    {
        arcaneBlast.chargeTime = 0.5f;
        arcaneBlast.damage = 20;
        arcaneShield.cooldown = 5;
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        mana = GetComponent<PlayerMana>();
    }

    private void Start()
    {
        EventSystem.events.OnMageArcaneBoltQuantityChosen += IncreaseBoltQuantity;
        EventSystem.events.OnMageArcaneBlastSpeedChosen += IncreaseBlastCastSpeed;
        EventSystem.events.OnMageArcaneBlastDamageChosen += IncreaseBlastDamage;
        EventSystem.events.OnMageArcaneShieldCooldownChosen += ReduceShieldCooldown;
        Resources.FindObjectsOfTypeAll<ManaBar>()[0].gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventSystem.events.OnMageArcaneBoltQuantityChosen -= IncreaseBoltQuantity;
        EventSystem.events.OnMageArcaneBlastSpeedChosen -= IncreaseBlastCastSpeed;
        EventSystem.events.OnMageArcaneBlastDamageChosen -= IncreaseBlastDamage;
        EventSystem.events.OnMageArcaneShieldCooldownChosen -= ReduceShieldCooldown;
    }

    void Update()
    {
        if (movement.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !boltOnCooldown)
                LaunchArcaneBolt(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                StartCoroutine(ChargeArcaneBlast());
            if (Input.GetKeyDown(KeyCode.Space) && !shieldOnCooldown)
                BeginShielding();
            if (Input.GetKeyUp(KeyCode.Space))
                StopShielding();
        }
    }

    private void LaunchArcaneBolt(Vector3 target)
    {
        StartCoroutine(Utils.Cooldown(result => boltOnCooldown = result, arcaneBolt.cooldown));
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        StartCoroutine(ArcaneBoltRoutine(boltQuantity, direction));
    }

    private IEnumerator ArcaneBoltRoutine(int quantity, Vector2 direction)
    {
        for (int i = 0; i < quantity; i++)
        {
            ArcaneBolt clone = Instantiate(arcaneBolt, body.transform);
            clone.GetComponent<Rigidbody2D>().AddForce(direction * boltForce);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ChargeArcaneBlast()
    {
        if (mana.currentMana >= arcaneBlast.manaCost)
        {
            DateTime start = DateTime.Now;
            while (Input.GetKey(KeyCode.Mouse1))
            {
                if ((DateTime.Now - start).TotalSeconds > arcaneBlast.chargeTime)
                {
                    LaunchArcaneBlast(Input.mousePosition);
                    break;
                }
                yield return null;
            }
        }
    }

    private void LaunchArcaneBlast(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        ArcaneBlast clone = Instantiate(arcaneBlast, body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * boltForce);
        mana.Lower(arcaneBlast.manaCost);
    }

    private void BeginShielding()
    {
        StartCoroutine(Utils.Cooldown(result => shieldOnCooldown = result, arcaneShield.cooldown));
        shieldClone = Instantiate(arcaneShield, body.transform);
    }

    private void StopShielding()
    {
        if (shieldClone)
            Destroy(shieldClone.gameObject);
    }

    private void IncreaseBoltQuantity(float amount)
    {
        boltQuantity += (int)amount;
    }

    private void IncreaseBlastCastSpeed(float amount)
    {
        arcaneBlast.chargeTime -= amount;
        if(arcaneBlast.chargeTime < 0)
            arcaneBlast.chargeTime = 0;
    }

    private void IncreaseBlastDamage(float amount)
    {
        arcaneBlast.damage += amount;
    }

    private void ReduceShieldCooldown(float amount)
    {
        arcaneShield.cooldown -= amount;
    }
}

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Character
{
    [SerializeField] private EnemyAttack attack;
    [SerializeField] private Explosion explosion;
    [SerializeField] private HealthPickup pickup;
    [NonSerialized] public bool explosive = false;
    private int attackForce = 200;
    private float iFramesDuation = 0.25f;
    private Rigidbody2D playerBody;

    private new void Awake()
    {
        base.Awake();
        movement = GetComponent<EnemyMovement>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventSystem.events.OnWarlockExplodeDots += Explode;
        EventSystem.events.OnPlayerDeath += DisableMovementForever;
    }

    private void OnDestroy()
    {
        if (UnityEngine.Random.value > 0.8f)
            Instantiate(pickup, body.transform.position, body.transform.rotation);
        EventSystem.events.OnWarlockExplodeDots -= Explode;
        EventSystem.events.OnPlayerDeath -= DisableMovementForever;
    }

    private void Update()
    {
        movement.Move();
        if (!mainAttackOnCooldown)
            Attack();
    }

    private void Attack()
    {
        StartCoroutine(Cooldown(result => mainAttackOnCooldown = result, mainAttackCooldown));
        Vector2 direction = (playerBody.position - body.position).normalized;
        EnemyAttack clone = Instantiate(attack, body.transform.position, body.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * attackForce);
    }

    public void Explode()
    {
        if(explosive)
            Instantiate(explosion, transform);
    }

    public IEnumerator IFrames()
    {
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Projectile");
        yield return new WaitForSeconds(iFramesDuation);
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask();
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }
}

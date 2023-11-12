using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Explosion explosion;
    [NonSerialized] public bool explosive = false;
    private float iFramesDuation = 0.25f;

    private new void Awake()
    {
        base.Awake();
        movement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        EventSystem.events.OnWarlockExplodeDots += Explode;
        EventSystem.events.OnPlayerDeath += DisableMovementForever;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnWarlockExplodeDots -= Explode;
        EventSystem.events.OnPlayerDeath -= DisableMovementForever;
    }

    private void Update()
    {
        movement.Move();
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

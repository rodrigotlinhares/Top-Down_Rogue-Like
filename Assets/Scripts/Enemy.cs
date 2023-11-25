using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Character
{
    [SerializeField] private Explosion explosion;
    [SerializeField] private HealthPickup pickup;
    [NonSerialized] public bool explosive = false;
    private float iFramesDuration = 0.25f;
    private EnemyFollowMovement movement;

    protected new void Awake()
    {
        base.Awake();
        movement = GetComponent<EnemyFollowMovement>();
    }

    protected void Start()
    {
        EventSystem.events.OnWarlockExplodeDots += Explode;
    }

    protected void OnDestroy()
    {
        if (UnityEngine.Random.value > 0.8f)
            Instantiate(pickup, body.transform.position, body.transform.rotation);
        EventSystem.events.OnWarlockExplodeDots -= Explode;
    }

    protected void Explode()
    {
        if(explosive)
            Instantiate(explosion, transform);
    }

    public IEnumerator IFrames()
    {
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Projectile");
        yield return new WaitForSeconds(iFramesDuration);
        GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask();
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }
}

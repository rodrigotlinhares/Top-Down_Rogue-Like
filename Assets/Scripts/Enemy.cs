using System;
using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Explosion explosion;
    [SerializeField] private HealthPickup pickup;
    [NonSerialized] public bool explosive = false;
    private Rigidbody2D body;
    private float iFramesDuration = 0.25f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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
}

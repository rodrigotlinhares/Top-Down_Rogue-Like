using System;
using System.Collections;
using UnityEngine;

public class Stab : PlayerAttack
{
    [SerializeField] private Explosion explosion;
    [SerializeField] private float duration;
    public float explosionChance;

    void Start()
    {
        StartCoroutine(Dissipate());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosionChance >= UnityEngine.Random.value)
            Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }

    IEnumerator Dissipate()
    {
        yield return new WaitForSeconds(duration);
        if(explosionChance >= UnityEngine.Random.value)
            Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}

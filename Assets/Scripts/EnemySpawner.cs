using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private int minDistance;

    private Character player;
    private Bounds bounds;

    private void Start()
    {
        player = FindObjectOfType<Character>();
        bounds = GameObject.Find("SpawnBounds").GetComponent<SpriteRenderer>().bounds;
        MakeEnemy();
    }

    private void MakeEnemy()
    {
        Vector2 position = new Vector2(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), UnityEngine.Random.Range(bounds.min.y, bounds.max.y));
        while (Vector2.Distance(position, player.GetComponent<Rigidbody2D>().position) < minDistance)
        {
            position.x = UnityEngine.Random.Range(bounds.min.x, bounds.max.y);
            position.y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        }

        Enemy clone = Instantiate(enemy, position, Quaternion.identity);
        clone.GetComponent<Health>().Death += MakeEnemy;
    }
}
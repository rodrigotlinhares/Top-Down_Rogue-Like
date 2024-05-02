using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private int minDistance;
    private int deaths = 0;
    private int difficulty = 1;
    private Character player;
    private Bounds bounds;

    private void Start()
    {
        player = FindObjectOfType<Character>();
        bounds = GameObject.Find("SpawnBounds").GetComponent<SpriteRenderer>().bounds;
        MakeEnemy();
        EventSystem.events.OnEnemyDeath += MakeEnemy;
        EventSystem.events.OnEnemyDeath += CountEnemyDeaths;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnEnemyDeath -= MakeEnemy;
        EventSystem.events.OnEnemyDeath -= CountEnemyDeaths;
    }

    private void CountEnemyDeaths()
    {
        deaths++;
        if (deaths%10 == 0)
        {
            if (difficulty < enemies.Length)
                difficulty++;
            MakeEnemy();
            EventSystem.events.StageCleared();
        }
    }

    private void MakeEnemy()
    {
        Vector2 position = new Vector2(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), UnityEngine.Random.Range(bounds.min.y, bounds.max.y));
        while (Vector2.Distance(position, player.GetComponent<Rigidbody2D>().position) < minDistance)
        {
            position.x = UnityEngine.Random.Range(bounds.min.x, bounds.max.y);
            position.y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        }

        //Instantiate(enemies[UnityEngine.Random.Range(0, difficulty)], position, Quaternion.identity);
        Instantiate(enemies[1], position, Quaternion.identity);
    }
}
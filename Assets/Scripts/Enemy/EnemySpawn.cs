using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject Enemy;

    float maxSpawnRateInSeconds = 5f;

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        GameObject anEnemy = (GameObject) Instantiate(Enemy);
        anEnemy.transform.position = new Vector2 (Random.Range(min.x,max.x),max.y);
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;
        if(maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f,maxSpawnRateInSeconds);
        }
        else
            spawnInNSeconds = 1f;
        Invoke ("SpawnEnemy",spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if(maxSpawnRateInSeconds > 1f)
        maxSpawnRateInSeconds--;

        if(maxSpawnRateInSeconds == 1f)
        CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleEnemySpawner()
    {
        Invoke ("SpawnEnemy",maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate",0f,30f);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}

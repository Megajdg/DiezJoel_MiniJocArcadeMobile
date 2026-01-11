using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string enemyPoolTag = "Enemy";

    public float spawnInterval = 3f;
    public int enemiesPerWave = 3;
    public float spawnRangeX = 11f;

    public bool useFormations = false;

    private bool spawning = false;

    public bool canSpawn = false;

    void Update()
    {
        if (!canSpawn) return;

        if (!spawning)
        {
            StartCoroutine(SpawnWaveRoutine());
            spawning = true;
        }
    }

    public void IncreaseDifficulty()
    {
        float difficulty = DifficultyManager.Instance.GetDifficulty();

        spawnInterval = Mathf.Max(0.5f, 3f / difficulty);

        enemiesPerWave = Mathf.Clamp(Mathf.RoundToInt(3 * difficulty), 3, 9);
    }

    void SpawnWave()
    {
        float difficulty = DifficultyManager.Instance.GetDifficulty();

        SpawnBasicEnemies();

        if (difficulty >= 1.5f && difficulty < 2.5f)
            SpawnZigZagEnemies();

        else if (difficulty >= 2.5f && difficulty < 3.5f)
            SpawnFollowEnemies();

        else if (difficulty >= 3.5f && difficulty < 4.5f)
            SpawnMixedWave();

        else if (difficulty >= 4.5f && difficulty < 5.5f)
            SpawnEliteTank();

        else if (difficulty >= 5.5f && difficulty < 6.5f)
            SpawnEliteFast();

        else if (difficulty >= 6.5f && difficulty < 7.5f)
            SpawnEliteBurst();

        else if (difficulty >= 7.5f)
            SpawnEliteMiniBoss();
    }

    IEnumerator SpawnWaveRoutine()
    {
        SpawnWave();
        yield return new WaitForSeconds(spawnInterval);
        spawning = false;
    }

    // ------------------------------------------------------
    // 1. Enemigos básicos (solo se mueven hacia abajo)
    // ------------------------------------------------------
    void SpawnBasicEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, 1, 0);
            pos.x += Random.Range(-spawnRangeX, spawnRangeX);

            GameObject enemy = ObjectPool.Instance.SpawnFromPool(enemyPoolTag, pos, Quaternion.identity);

            // Aseguramos que solo tenga EnemyController
            RemoveMovementComponents(enemy);
        }
    }

    // ------------------------------------------------------
    // 2. Enemigos ZigZag (añade ZigZagMovement)
    // ------------------------------------------------------
    void SpawnZigZagEnemies()
    {
        for (int i = 0; i < Mathf.CeilToInt(enemiesPerWave * 0.5f); i++)
        {
            Vector3 pos = transform.position + new Vector3(0, 1, 0);
            pos.x += Random.Range(-spawnRangeX, spawnRangeX);

            GameObject enemy = ObjectPool.Instance.SpawnFromPool(enemyPoolTag, pos, Quaternion.identity);

            RemoveMovementComponents(enemy);
            enemy.AddComponent<ZigZagMovement>();
        }
    }

    // ------------------------------------------------------
    // 3. Enemigos que siguen al jugador (FollowPlayer)
    // ------------------------------------------------------
    void SpawnFollowEnemies()
    {
        for (int i = 0; i < Mathf.CeilToInt(enemiesPerWave * 0.3f); i++)
        {
            Vector3 pos = transform.position + new Vector3(0, 1, 0);
            pos.x += Random.Range(-spawnRangeX, spawnRangeX);

            GameObject enemy = ObjectPool.Instance.SpawnFromPool(enemyPoolTag, pos, Quaternion.identity);

            RemoveMovementComponents(enemy);
            enemy.AddComponent<FollowPlayer>();
        }
    }

    // ------------------------------------------------------
    // 4. Horda mixta (mezcla de tipos según dificultad)
    // ------------------------------------------------------
    void SpawnMixedWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, 1, 0);
            pos.x += Random.Range(-spawnRangeX, spawnRangeX);

            GameObject enemy = ObjectPool.Instance.SpawnFromPool(enemyPoolTag, pos, Quaternion.identity);

            RemoveMovementComponents(enemy);

            int type = Random.Range(0, 2);

            switch (type)
            {
                case 0:
                    enemy.AddComponent<ZigZagMovement>();
                    break;

                case 1:
                    enemy.AddComponent<FollowPlayer>();
                    break;
            }
        }
    }

    void SpawnEliteTank()
    {
        SpawnEliteEnemy<EnemyTank>();
    }

    void SpawnEliteFast()
    {
        SpawnEliteEnemy<EnemyFast>();
    }

    void SpawnEliteBurst()
    {
        SpawnEliteEnemy<EnemyBurst>();
    }

    void SpawnEliteMiniBoss()
    {
        SpawnEliteEnemy<EnemyMiniBoss>();

        DifficultyManager.Instance.ResetDifficultyCycle();
    }

    void SpawnEliteEnemy<T>() where T : Component
    {
        Vector3 pos = transform.position + new Vector3(0, 1, 0);
        pos.x += Random.Range(-spawnRangeX, spawnRangeX);

        GameObject enemy = ObjectPool.Instance.SpawnFromPool(enemyPoolTag, pos, Quaternion.identity);

        RemoveMovementComponents(enemy);

        if (enemy.GetComponent<T>() == null)
            enemy.AddComponent<T>();

        EnemyHealth hp = enemy.GetComponent<EnemyHealth>();
        if (hp != null)
        {
            if (typeof(T) == typeof(EnemyTank)) hp.maxHealth = 10;
            if (typeof(T) == typeof(EnemyFast)) hp.maxHealth = 2;
            if (typeof(T) == typeof(EnemyBurst)) hp.maxHealth = 4;
            if (typeof(T) == typeof(EnemyMiniBoss)) hp.maxHealth = 20;
        }
    }


    void RemoveMovementComponents(GameObject enemy)
    {
        ZigZagMovement zig = enemy.GetComponent<ZigZagMovement>();
        if (zig != null) Destroy(zig);

        FollowPlayer follow = enemy.GetComponent<FollowPlayer>();
        if (follow != null) Destroy(follow);
    }
}

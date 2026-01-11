using UnityEngine;
using System.Collections;

public class EnemyMiniBoss : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float RPS = 1f;

    private bool shooting = false;

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (!shooting)
        {
            StartCoroutine(ShootRoutine());
            shooting = true;
        }
    }

    IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(1f / RPS);

        for (int i = -2; i <= 2; i++)
        {
            GameObject bullet = ObjectPool.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.Euler(0, i * 10, 0));
            bullet.GetComponent<Bullet>().direction = Quaternion.Euler(0, i * 10, 0) * Vector3.back;

            float difficulty = DifficultyManager.Instance.GetDifficulty();
            bullet.GetComponent<Bullet>().speed = 15f * difficulty;
        }

        shooting = false;
    }
}

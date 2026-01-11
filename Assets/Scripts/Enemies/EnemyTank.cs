using System.Collections;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float RPS = 0.5f;

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

        GameObject bullet = ObjectPool.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = Vector3.back;

        float difficulty = DifficultyManager.Instance.GetDifficulty();
        bullet.GetComponent<Bullet>().speed = 15f * difficulty;

        shooting = false;
    }
}

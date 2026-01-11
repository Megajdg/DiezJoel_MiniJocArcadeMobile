using UnityEngine;
using System.Collections;

public class EnemyBurst : MonoBehaviour
{
    public float burstInterval = 2f;
    public int bulletsPerBurst = 4;

    private bool shooting = false;

    void Update()
    {
        if (!shooting)
        {
            StartCoroutine(BurstRoutine());
            shooting = true;
        }
    }

    IEnumerator BurstRoutine()
    {
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            GameObject bullet = ObjectPool.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = Vector3.back;

            float difficulty = DifficultyManager.Instance.GetDifficulty();
            bullet.GetComponent<Bullet>().speed = 15f * difficulty;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(burstInterval);
        shooting = false;
    }
}

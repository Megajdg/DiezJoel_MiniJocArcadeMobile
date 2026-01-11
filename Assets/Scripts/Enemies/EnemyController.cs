using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform bulletSpawn;
    public string bulletPoolTag = "EnemyBullet";

    public float RPS = 0.75f;
    public float moveSpeed = 2f;

    private bool shooting = false;

    private void OnEnable()
    {
        shooting = false;    
    }

    void Update()
    {
        float difficulty = DifficultyManager.Instance.GetDifficulty();

        float currentSpeed = moveSpeed * difficulty;
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

        float currentRPS = RPS * difficulty;

        if (!shooting)
        {
            StartCoroutine(Shoot(1f / currentRPS));
            shooting = true;
        }
    }

    IEnumerator Shoot(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject bullet = ObjectPool.Instance.SpawnFromPool(bulletPoolTag, bulletSpawn.position, bulletSpawn.rotation);
        
        if (bullet != null)
        {
            Bullet b = bullet.GetComponent<Bullet>();
            if (b != null)
            {
                b.direction = Vector3.back;
            }

            float difficulty = DifficultyManager.Instance.GetDifficulty();
            b.speed = 5f * difficulty;
        }

        shooting = false;
    }
}

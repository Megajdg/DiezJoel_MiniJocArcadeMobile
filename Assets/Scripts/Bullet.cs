using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 1;
    public Vector3 direction = Vector3.forward;

    public bool piercing = false;

    private float timer = 0f;

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ParticleManager.Instance.Spawn(
        ParticleManager.Instance.bulletImpactFX,
        transform.position
        );

        if (gameObject.CompareTag("PlayerBullet"))
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyHealth enemy = other.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);

                    if (!piercing)
                        gameObject.SetActive(false);
                }
            }
        }

        if (gameObject.CompareTag("EnemyBullet"))
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth ph = other.GetComponent<PlayerHealth>();
                if (ph != null) ph.TakeDamage(damage);

                gameObject.SetActive(false);
            }
            return;
        }
    }
}

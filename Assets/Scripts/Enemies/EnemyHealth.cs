using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    public GameObject[] powerUpPrefabs;
    public float dropChance = 0.1f;

    public HealthBarUI healthBar;

    void OnEnable()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    private void Start()
    {
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        AudioManager.Instance.Play(AudioManager.Instance.hitSFX);

        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ParticleManager.Instance.Spawn(
        ParticleManager.Instance.enemyDeathFX,
        transform.position
        );

        if (Random.value < dropChance)
        {
            int index = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[index], transform.position, Quaternion.identity);
            AudioManager.Instance.Play(AudioManager.Instance.powerUpSpawnSFX);
        }

        AudioManager.Instance.Play(AudioManager.Instance.enemyDeathSFX);

        ScoreManager.Instance.AddScore(1);

        gameObject.SetActive(false);
    }
}

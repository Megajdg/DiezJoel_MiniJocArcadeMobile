using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Invulnerability")]
    public float invulnDuration = 1f;
    private bool invulnerable = false;

    private PlayerPowerUps powerUps;

    private Color originalColor;

    public HealthBarUI healthBar;

    public ScreenEffects effects;

    void OnEnable()
    {
        currentHealth = maxHealth;
        invulnerable = false;
    }

    private void Start()
    {
        powerUps = GetComponent<PlayerPowerUps>();

        healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int dmg)
    {
        if (invulnerable)
            return;

        if (powerUps != null && powerUps.shield)
        {
            powerUps.shield = false;
            return;
        }

        currentHealth -= dmg;

        AudioManager.Instance.Play(AudioManager.Instance.playerHurtSFX);

        ParticleManager.Instance.Spawn(
        ParticleManager.Instance.playerHurtFX,
        transform.position
        );

        healthBar.SetHealth(currentHealth, maxHealth);

        effects.FlashRed();
        effects.CameraShake();
        effects.Vibrate();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }

    IEnumerator Invulnerability()
    {
        invulnerable = true;

        yield return new WaitForSeconds(invulnDuration * 0.5f);

        yield return new WaitForSeconds(invulnDuration * 0.5f);

        invulnerable = false;
    }

    void Die()
    {
        PlayerPowerUps p = GetComponent<PlayerPowerUps>();

        if (p != null && p.extraLives > 0)
        {
            p.extraLives--;
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth, maxHealth);
            StartCoroutine(Invulnerability());
            return;
        }

        gameObject.SetActive(false);

        AudioManager.Instance.Play(AudioManager.Instance.gameOverSFX);

        int finalScore = ScoreManager.Instance.GetScore();
        GameOverUI.Instance.ShowGameOver(finalScore);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

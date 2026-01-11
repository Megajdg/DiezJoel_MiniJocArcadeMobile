using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public bool piercing = false;
    public bool multiShot = false;
    public bool spread = false;
    public bool shield = false;

    public float fireRateMultiplier = 1f;

    public int extraLives = 0;

    public GameObject shieldVisual;

    float fireRateTimer = 0f;
    float piercingTimer = 0f;
    float multiShotTimer = 0f;
    float spreadTimer = 0f;
    float shieldTimer = 0f;

    void Update()
    {
        // FIRE RATE
        if (fireRateTimer > 0)
        {
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0)
                fireRateMultiplier = 1f;
        }

        // PIERCING
        if (piercingTimer > 0)
        {
            piercingTimer -= Time.deltaTime;
            if (piercingTimer <= 0)
                piercing = false;
        }

        // MULTISHOT
        if (multiShotTimer > 0)
        {
            multiShotTimer -= Time.deltaTime;
            if (multiShotTimer <= 0)
                multiShot = false;
        }

        // SPREAD
        if (spreadTimer > 0)
        {
            spreadTimer -= Time.deltaTime;
            if (spreadTimer <= 0)
                spread = false;
        }

        // SHIELD
        if (shieldTimer > 0)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                shield = false;
                shieldVisual.SetActive(false);
            }
        }
    }

    public void ActivatePowerUp(PowerUp.PowerUpType type, float duration)
    {
        AudioManager.Instance.Play(AudioManager.Instance.powerUpGetSFX);
        ParticleManager.Instance.Spawn(
        ParticleManager.Instance.powerUpFX,
        transform.position
        );

        switch (type)
        {
            case PowerUp.PowerUpType.FireRate:
                fireRateMultiplier = 2f;
                fireRateTimer = duration;
                break;

            case PowerUp.PowerUpType.Piercing:
                piercing = true;
                piercingTimer = duration;
                break;

            case PowerUp.PowerUpType.MultiShot:
                multiShot = true;
                multiShotTimer = duration;
                break;

            case PowerUp.PowerUpType.Spread:
                spread = true;
                spreadTimer = duration;
                break;
            
            case PowerUp.PowerUpType.Shield:
                shield = true;
                shieldTimer = duration;
                shieldVisual.SetActive(true);
                break;

            case PowerUp.PowerUpType.ExtraLife:
                extraLives++;
                break;
        }
    }
}

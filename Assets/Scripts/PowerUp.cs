using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { FireRate, Piercing, MultiShot, Spread, Shield, ExtraLife }
    public PowerUpType type;

    public float duration = 10f;
    public float fallSpeed = 10f;

    private AudioSource loopSource;

    private void Start()
    {
        loopSource = GetComponent<AudioSource>();

        if (loopSource != null)
            loopSource.Play();
    }

    void Update()
    {
        transform.Translate(Vector3.back * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUps p = other.GetComponent<PlayerPowerUps>();
            if (p != null)
            {
                p.ActivatePowerUp(type, duration);
            }

            if (loopSource != null)
                loopSource.Stop();

            gameObject.SetActive(false);
        }
    }
}

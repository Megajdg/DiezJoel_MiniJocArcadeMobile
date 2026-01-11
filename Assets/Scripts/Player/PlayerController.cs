using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour {

    public InputAction touchInputAction;
    public InputAction wasdInputAction;
    
    public Transform bulletSpawn;
    public string bulletPoolTag = "PlayerBullet";

    public float moveSpeed = 5f;
    public float RPS = 1f;

    private Rigidbody rb;
    private bool shooting = false;

    private PlayerPowerUps powerUps;

    public bool canPlay = false;

    private void OnEnable() {
        touchInputAction.Enable();
        wasdInputAction.Enable();
        powerUps = GetComponent<PlayerPowerUps>();
    }

    private void OnDisable() {
        touchInputAction.Disable();
        wasdInputAction.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.player = transform;
        }
    }

    void Update() {

        if (!canPlay) {
            rb.velocity = Vector3.zero;
            return;
        }

        Vector2 touchDelta = touchInputAction.ReadValue<Vector2>();
        float horizontal = wasdInputAction.ReadValue<float>();

        float finalHorizontal = 0f;

        if (touchDelta != Vector2.zero)
        {
            finalHorizontal = touchDelta.x * 0.25f;
        }
        else
        {
            finalHorizontal = horizontal * moveSpeed;
        }

        rb.velocity = new Vector2(finalHorizontal, 0f);
        
        if (!shooting)
        {
            StartCoroutine(ShootRoutine());
            shooting = true;
        }
    }

    void Shoot()
    {
        FireBullet(Vector3.forward);

        if (powerUps.multiShot)
        {
            FireBullet(Vector3.forward, -0.5f);
            FireBullet(Vector3.forward, 0.5f);
        }

        // 3. Spread (abanico)
        if (powerUps.spread)
        {
            FireBullet(Quaternion.Euler(0, -10, 0) * Vector3.forward);
            FireBullet(Quaternion.Euler(0, 10, 0) * Vector3.forward);
        }
    }


    IEnumerator ShootRoutine()
    {
        float d = DifficultyManager.Instance.GetDifficulty();
        float finalRPS = RPS * d * powerUps.fireRateMultiplier;

        yield return new WaitForSeconds(1f / finalRPS);

        Shoot();
        AudioManager.Instance.Play(AudioManager.Instance.shootSFX);
        shooting = false;
    }

    void FireBullet(Vector3 direction, float offsetX = 0f)
    {
        Vector3 spawnPos = bulletSpawn.position + new Vector3(offsetX, 0, 0);

        GameObject bullet = ObjectPool.Instance.SpawnFromPool("PlayerBullet", spawnPos, Quaternion.identity);

        Bullet b = bullet.GetComponent<Bullet>();
        b.direction = direction;
        b.speed = 25f * DifficultyManager.Instance.GetDifficulty();
        b.damage = 1;
        b.piercing = powerUps.piercing;
    }

}

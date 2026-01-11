using UnityEngine;

public class RemoverController : MonoBehaviour
{
    public int damageToPlayer = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            other.gameObject.SetActive(false);
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            PlayerHealth player = GameManager.Instance.player.GetComponent<PlayerHealth>();
            player.TakeDamage(damageToPlayer);

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemy.Die();

            return;
        }
    }
}

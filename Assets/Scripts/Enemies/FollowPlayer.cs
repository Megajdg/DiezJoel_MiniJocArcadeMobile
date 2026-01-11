using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.player == null)
            return;

        Transform player = GameManager.Instance.player;

        Vector3 dir = (player.position - transform.position).normalized;
        Vector3 move = new Vector3(dir.x, 0f, 0f) * speed * Time.deltaTime;

        transform.position += move;
    }
}

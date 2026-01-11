using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    public GameObject bulletImpactFX;
    public GameObject enemyDeathFX;
    public GameObject playerHurtFX;
    public GameObject powerUpFX;

    void Awake()
    {
        Instance = this;
    }

    public void Spawn(GameObject fx, Vector3 pos)
    {
        if (fx != null)
            Instantiate(fx, pos, Quaternion.identity);
    }
}
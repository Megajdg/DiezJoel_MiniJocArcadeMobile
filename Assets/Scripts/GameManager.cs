using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform player;
    public float difficultyInterval = 20f;

    private EnemySpawner enemySpawner;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();

        if (difficultyInterval > 0)
        {
            InvokeRepeating(nameof(IncreaseDifficulty), difficultyInterval, difficultyInterval);
        }
    }

    void IncreaseDifficulty()
    {
        if (enemySpawner != null)
        {
            enemySpawner.IncreaseDifficulty();
        }
    }
}

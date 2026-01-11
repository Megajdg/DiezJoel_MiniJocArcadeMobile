using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    [Header("Difficulty Settings")]
    public float difficulty = 1f;
    public float difficultyGrowth = 0.1f;
    public float interval = 10f;

    private float timer = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            difficulty += difficultyGrowth;
        }
    }

    public float GetDifficulty()
    {
        return difficulty;
    }

    public void ResetDifficultyCycle()
    {
        difficulty = 1f;
    }
}

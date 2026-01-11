using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    public GameObject panel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowGameOver(int finalScore)
    {
        panel.SetActive(true);

        AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);

        panel.transform.localScale = Vector3.zero;
        
        panel.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack).SetUpdate(true);

        scoreText.text = "Score: " + finalScore;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        highScoreText.text = "High Score: " + highScore;

        GameManager.Instance.player.GetComponent<PlayerController>().canPlay = false;
        FindObjectOfType<EnemySpawner>().canSpawn = false;
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        foreach (EnemyHealth e in enemies) {
            e.gameObject.SetActive(false);
        }
    }

    public void Restart()
    {
        panel.transform.DOScale(0.7f, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                panel.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
    }

    public void ToMenu()
    {
        panel.transform.DOScale(0.7f, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                panel.SetActive(false);
                StartUI.hasStarted = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
    }
}

using UnityEngine;
using DG.Tweening;

public class StartUI : MonoBehaviour
{
    public static bool hasStarted = false;

    public GameObject panel;

    private bool gameActivated = false;

    void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);

        if (!hasStarted)
        {
            panel.SetActive(true);

            panel.transform.localScale = Vector3.zero;
            panel.transform.DOScale(1f, 0.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (gameActivated)
            return;

        if (hasStarted && GameManager.Instance.player != null && FindObjectOfType<EnemySpawner>() != null)
        {
            ActivateGame();
        }
    }

    void ActivateGame()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusic);

        gameActivated = true;

        Time.timeScale = 1f;

        GameManager.Instance.player.GetComponent<PlayerController>().canPlay = true;
        FindObjectOfType<EnemySpawner>().canSpawn = true;
    }

    public void StartGame()
    {
        hasStarted = true;

        panel.transform.DOScale(0.7f, 0.3f)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                panel.SetActive(false);
                ActivateGame();
            });
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

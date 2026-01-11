using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;

    private PlayerPowerUps powerUps;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            powerUps = GameManager.Instance.player.GetComponent<PlayerPowerUps>();
        }
        else
        {
            return;
        }

        livesText.text = "Extra Lives: " + powerUps.extraLives;
    }
}

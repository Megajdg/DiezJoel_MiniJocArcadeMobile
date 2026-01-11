using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenEffects : MonoBehaviour
{
    public Image damageFlash;

    public void FlashRed()
    {
        damageFlash.color = new Color(1, 0, 0, 0);

        damageFlash.DOFade(0.4f, 0.05f).SetUpdate(true);

        damageFlash.DOFade(0f, 0.3f).SetDelay(0.05f).SetUpdate(true);
    }

    public void CameraShake()
    {
        Camera.main.transform.DOShakePosition(0.2f, 0.4f, 20, 90f).SetUpdate(true);
    }

    public void Vibrate()
    {
    #if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
    #endif
    }
}

using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip shootSFX;
    public AudioClip hitSFX;
    public AudioClip enemyDeathSFX;
    public AudioClip playerHurtSFX;
    public AudioClip powerUpGetSFX;
    public AudioClip powerUpSpawnSFX;
    public AudioClip powerUpLoopSFX;
    public AudioClip gameOverSFX;

    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        Instance = this;

        AudioSource[] sources = GetComponents<AudioSource>();
        sfxSource = sources[0];
        musicSource = sources[1];

        musicSource.loop = true;

        musicSource.ignoreListenerPause = true;
    }

    public void Play(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource.clip == clip) return;

        StartCoroutine(FadeMusic(clip));
    }

    IEnumerator FadeMusic(AudioClip newClip)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            musicSource.volume = 1f - t;
            yield return null;
        }

        musicSource.clip = newClip;
        musicSource.Play();

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            musicSource.volume = t;
            yield return null;
        }
    }
}

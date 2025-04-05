using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip[] sfxClips;

    private void Start()
    {
        PlayMusic(true);
    }

    public enum SFXType
    {
        Walk,
        Shoot,
        Explosion,
        Jump
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(bool isGameplay)
    {
        musicSource.clip = isGameplay ? gameplayMusic : menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(SFXType sfxType)
    {
        sfxSource.PlayOneShot(sfxClips[(int)sfxType]);
    }

    // Detener un SFX específico
    public void StopSFX(SFXType sfxType)
    {
        sfxSource.Stop();
    }

    // Verificar si un SFX ya está siendo reproducido
    public bool IsPlayingSFX(SFXType sfxType)
    {
        return sfxSource.isPlaying && sfxSource.clip == sfxClips[(int)sfxType];
    }
}

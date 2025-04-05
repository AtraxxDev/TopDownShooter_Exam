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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        PlayMusic(false);
    }

    public enum SFXType
    {
        Walk,
        Shoot,
        Explosion,
        Jump
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

    public void StopSFX(SFXType sfxType)
    {
        sfxSource.Stop();
    }

    public bool IsPlayingSFX(SFXType sfxType)
    {
        return sfxSource.isPlaying && sfxSource.clip == sfxClips[(int)sfxType];
    }
}

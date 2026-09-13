using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource musicSource; // looping ambient music

    public AudioClip fireSound;
    public AudioClip dockSound;
    public AudioClip failSound;
    public AudioClip musicTrack;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // stops duplicates if this scene reloads
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (musicTrack != null)
        {
            musicSource.clip = musicTrack;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayFire() => sfxSource.PlayOneShot(fireSound);
    public void PlayDock() => sfxSource.PlayOneShot(dockSound);
    public void PlayFail() => sfxSource.PlayOneShot(failSound);
  
    }
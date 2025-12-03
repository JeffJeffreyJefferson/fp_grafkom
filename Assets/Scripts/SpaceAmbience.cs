using UnityEngine;

/// <summary>
/// Script untuk mengelola audio ambience space yang di-loop
/// Attach ke Main Camera atau empty GameObject
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SpaceAmbience : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip ambienceClip;          // Clip audio ambience
    [Range(0f, 1f)]
    public float volume = 0.5f;             // Volume audio
    public bool playOnStart = true;         // Play otomatis saat start
    public bool loop = true;                // Loop audio
    
    [Header("Fade Settings")]
    public bool fadeIn = true;              // Fade in saat mulai
    public float fadeInDuration = 2f;       // Durasi fade in
    
    private AudioSource audioSource;
    private float targetVolume;
    private bool isFading;

    void Awake()
    {
        SetupAudioSource();
    }
    
    void Start()
    {
        if (playOnStart && ambienceClip != null)
        {
            PlayAmbience();
        }
    }
    
    void Update()
    {
        // Handle fade in
        if (isFading && audioSource.volume < targetVolume)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.deltaTime / fadeInDuration);
            if (audioSource.volume >= targetVolume)
            {
                isFading = false;
            }
        }
    }
    
    void SetupAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.clip = ambienceClip;
        audioSource.loop = loop;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound
        audioSource.priority = 128;
    }
    
    /// <summary>
    /// Play ambience dengan fade in
    /// </summary>
    public void PlayAmbience()
    {
        if (audioSource == null || ambienceClip == null) return;
        
        audioSource.clip = ambienceClip;
        
        if (fadeIn)
        {
            audioSource.volume = 0f;
            targetVolume = volume;
            isFading = true;
        }
        else
        {
            audioSource.volume = volume;
        }
        
        audioSource.Play();
    }
    
    /// <summary>
    /// Stop ambience
    /// </summary>
    public void StopAmbience()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    
    /// <summary>
    /// Pause ambience
    /// </summary>
    public void PauseAmbience()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }
    
    /// <summary>
    /// Resume ambience
    /// </summary>
    public void ResumeAmbience()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
    
    /// <summary>
    /// Set volume
    /// </summary>
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        targetVolume = volume;
        
        if (!isFading && audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
    
    /// <summary>
    /// Set audio clip
    /// </summary>
    public void SetAmbienceClip(AudioClip clip)
    {
        ambienceClip = clip;
        if (audioSource != null)
        {
            audioSource.clip = clip;
        }
    }
}

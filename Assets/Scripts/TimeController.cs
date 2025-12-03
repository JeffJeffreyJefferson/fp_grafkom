using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script untuk mengontrol waktu simulasi (pause, play, fast forward)
/// BEKERJA OUT OF THE BOX dengan keyboard shortcuts!
/// Keyboard: Space = Pause/Play, Right Arrow = Fast, Left Arrow = Slow, R = Reset
/// </summary>
public class TimeController : MonoBehaviour
{
    [Header("UI References (Opsional)")]
    public Button playButton;
    public Button pauseButton;
    public Button fastForwardButton;
    public Button slowMotionButton;
    public Text timeScaleText;
    
    [Header("Time Settings")]
    public float normalTimeScale = 1f;
    public float fastTimeScale = 3f;
    public float slowTimeScale = 0.3f;
    
    [Header("State")]
    public bool isPaused = false;
    
    [Header("Debug")]
    public bool showDebugInfo = true;
    
    private float currentTimeScale = 1f;

    void Start()
    {
        SetupButtons();
        currentTimeScale = normalTimeScale;
        Time.timeScale = currentTimeScale;
        UpdateTimeScaleDisplay();
        
        if (showDebugInfo)
        {
            Debug.Log("[TimeController] Ready! Keyboard shortcuts:");
            Debug.Log("  Space = Pause/Play");
            Debug.Log("  Right Arrow = Fast Forward (3x)");
            Debug.Log("  Left Arrow = Slow Motion (0.3x)");
            Debug.Log("  R = Reset to Normal Speed");
        }
    }
    
    void SetupButtons()
    {
        if (playButton != null)
            playButton.onClick.AddListener(Play);
        
        if (pauseButton != null)
            pauseButton.onClick.AddListener(Pause);
        
        if (fastForwardButton != null)
            fastForwardButton.onClick.AddListener(FastForward);
        
        if (slowMotionButton != null)
            slowMotionButton.onClick.AddListener(SlowMotion);
    }
    
    void Update()
    {
        // Keyboard shortcuts - SELALU AKTIF!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
            if (showDebugInfo) Debug.Log($"[TimeController] {(isPaused ? "PAUSED" : "PLAYING")}");
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FastForward();
            if (showDebugInfo) Debug.Log("[TimeController] Fast Forward (3x)");
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SlowMotion();
            if (showDebugInfo) Debug.Log("[TimeController] Slow Motion (0.3x)");
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTimeScale();
            if (showDebugInfo) Debug.Log("[TimeController] Reset to Normal Speed");
        }
    }
    
    public void Play()
    {
        isPaused = false;
        Time.timeScale = currentTimeScale;
        UpdateTimeScaleDisplay();
    }
    
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        UpdateTimeScaleDisplay();
    }
    
    public void TogglePause()
    {
        if (isPaused)
            Play();
        else
            Pause();
    }
    
    public void FastForward()
    {
        isPaused = false;
        currentTimeScale = fastTimeScale;
        Time.timeScale = currentTimeScale;
        UpdateTimeScaleDisplay();
    }
    
    public void SlowMotion()
    {
        isPaused = false;
        currentTimeScale = slowTimeScale;
        Time.timeScale = currentTimeScale;
        UpdateTimeScaleDisplay();
    }
    
    public void ResetTimeScale()
    {
        isPaused = false;
        currentTimeScale = normalTimeScale;
        Time.timeScale = currentTimeScale;
        UpdateTimeScaleDisplay();
    }
    
    public void SetTimeScale(float scale)
    {
        currentTimeScale = scale;
        if (!isPaused)
        {
            Time.timeScale = scale;
        }
        UpdateTimeScaleDisplay();
    }
    
    void UpdateTimeScaleDisplay()
    {
        if (timeScaleText != null)
        {
            if (isPaused)
                timeScaleText.text = "PAUSED";
            else
                timeScaleText.text = $"Time: {currentTimeScale:F1}x";
        }
    }
    
    void OnGUI()
    {
        // Tampilkan info di sudut kiri atas jika tidak ada UI Text
        if (timeScaleText == null && showDebugInfo)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 16;
            style.normal.textColor = Color.white;
            
            string status = isPaused ? "PAUSED" : $"Speed: {currentTimeScale:F1}x";
            GUI.Label(new Rect(10, 10, 200, 30), status, style);
            GUI.Label(new Rect(10, 30, 300, 20), "Space=Pause | ←→=Speed | R=Reset", style);
        }
    }
}

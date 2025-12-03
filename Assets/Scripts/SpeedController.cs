using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Controller untuk UI Slider yang mengatur kecepatan semua planet
/// 
/// SETUP:
/// 1. Buat Empty GameObject → Add Component → SpeedController
/// 2. Drag Slider ke field "Speed Slider"
/// 3. Drag Text ke field "Speed Text" (opsional)
/// 4. Play!
/// 
/// JIKA SLIDER TIDAK BISA DIKLIK:
/// - Pastikan Slider TIDAK tertutup UI element lain
/// - Cek apakah ada Image dengan Raycast Target = true yang menutupi
/// - Pastikan Slider paling atas di hierarchy Canvas
/// </summary>
public class SpeedController : MonoBehaviour
{
    [Header("UI References - WAJIB DIISI!")]
    [Tooltip("Drag Slider dari Hierarchy ke sini")]
    public Slider speedSlider;
    
    [Tooltip("Drag Text dari Hierarchy ke sini (opsional)")]
    public Text speedText;
    
    [Header("Speed Settings")]
    public float minSpeed = 0.1f;
    public float maxSpeed = 10f;
    public float defaultSpeed = 1f;
    
    [Header("Auto-Detect")]
    [Tooltip("Planet akan auto-detect saat Play")]
    public PlanetController[] planets;
    
    private RotateAround[] rotators;
    private int[] originalRotatorSpeeds;
    
    void Awake()
    {
        EnsureUISystem();
    }
    
    void Start()
    {
        Initialize();
    }
    
    void Initialize()
    {
        // Auto-detect PlanetController
        if (planets == null || planets.Length == 0)
        {
            planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.None);
        }
        
        // Auto-detect RotateAround (backward compatibility)
        rotators = FindObjectsByType<RotateAround>(FindObjectsSortMode.None);
        if (rotators != null && rotators.Length > 0)
        {
            originalRotatorSpeeds = new int[rotators.Length];
            for (int i = 0; i < rotators.Length; i++)
            {
                if (rotators[i] != null)
                    originalRotatorSpeeds[i] = rotators[i].speed;
            }
        }
        
        // Setup slider
        if (speedSlider != null)
        {
            speedSlider.minValue = minSpeed;
            speedSlider.maxValue = maxSpeed;
            speedSlider.value = defaultSpeed;
            speedSlider.interactable = true;
            
            // Hapus listener lama, tambah baru
            speedSlider.onValueChanged.RemoveAllListeners();
            speedSlider.onValueChanged.AddListener(OnSpeedChanged);
        }
        else
        {
            Debug.LogError("[SpeedController] SLIDER BELUM DI-ASSIGN! Drag Slider ke field 'Speed Slider' di Inspector.");
        }
        
        UpdateSpeedText(defaultSpeed);
    }
    
    void EnsureUISystem()
    {
        // Pastikan EventSystem ada
        if (FindAnyObjectByType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }
        
        // Pastikan Canvas punya GraphicRaycaster
        Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        foreach (var canvas in canvases)
        {
            if (canvas.GetComponent<GraphicRaycaster>() == null)
            {
                canvas.gameObject.AddComponent<GraphicRaycaster>();
            }
        }
    }
    
    public void OnSpeedChanged(float value)
    {
        // Update PlanetController
        if (planets != null)
        {
            foreach (var planet in planets)
            {
                if (planet != null)
                    planet.SetSpeedMultiplier(value);
            }
        }
        
        // Update RotateAround
        if (rotators != null && originalRotatorSpeeds != null)
        {
            for (int i = 0; i < rotators.Length; i++)
            {
                if (rotators[i] != null)
                    rotators[i].speed = Mathf.RoundToInt(originalRotatorSpeeds[i] * value);
            }
        }
        
        UpdateSpeedText(value);
    }
    
    void UpdateSpeedText(float value)
    {
        if (speedText != null)
        {
            speedText.text = $"Speed: {value:F1}x";
        }
    }
    
    public void ResetSpeed()
    {
        if (speedSlider != null)
            speedSlider.value = defaultSpeed;
    }
}

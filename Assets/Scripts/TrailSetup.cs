using UnityEngine;

/// <summary>
/// Script untuk mengatur Trail Renderer pada planet
/// Membuat efek visual garis lintasan saat planet bergerak
/// 
/// CARA PAKAI:
/// 1. Add Component → TrailSetup ke planet
/// 2. Set Start Color dan End Color di Inspector
/// 3. Play!
/// </summary>
[RequireComponent(typeof(TrailRenderer))]
[ExecuteInEditMode]
public class TrailSetup : MonoBehaviour
{
    [Header("Trail Settings")]
    public float trailTime = 3f;            // Durasi trail
    public float startWidth = 0.3f;         // Lebar awal trail
    public float endWidth = 0.05f;          // Lebar akhir trail
    
    [Header("Trail Color - SET INI!")]
    [Tooltip("Warna awal trail - Alpha harus 255!")]
    public Color startColor = new Color(0.3f, 0.5f, 1f, 1f);  // Default biru, alpha FULL
    [Tooltip("Warna akhir trail - Alpha harus 0!")]
    public Color endColor = new Color(0.3f, 0.5f, 1f, 0f);    // Default biru transparan
    
    [Header("Advanced")]
    [Tooltip("Centang untuk update warna saat Play mode")]
    public bool updateColorsRealtime = true;
    
    private TrailRenderer trailRenderer;
    private Color lastStartColor;
    private Color lastEndColor;

    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        ApplySettings();
    }
    
    void Start()
    {
        ApplySettings();
    }
    
    void Update()
    {
        // Update warna secara realtime jika berubah di Inspector
        if (updateColorsRealtime)
        {
            if (lastStartColor != startColor || lastEndColor != endColor)
            {
                ApplyColors();
                lastStartColor = startColor;
                lastEndColor = endColor;
            }
        }
    }
    
    void OnValidate()
    {
        // Dipanggil saat nilai berubah di Inspector (Editor mode)
        if (trailRenderer == null)
            trailRenderer = GetComponent<TrailRenderer>();
            
        if (trailRenderer != null)
        {
            ApplySettings();
        }
    }
    
    void ApplySettings()
    {
        if (trailRenderer == null)
            trailRenderer = GetComponent<TrailRenderer>();
            
        if (trailRenderer == null) return;
        
        // Basic settings
        trailRenderer.time = trailTime;
        trailRenderer.startWidth = startWidth;
        trailRenderer.endWidth = endWidth;
        trailRenderer.minVertexDistance = 0.1f;
        trailRenderer.autodestruct = false;
        
        // Apply colors
        ApplyColors();
        
        // Material - gunakan Sprites/Default untuk warna solid
        if (trailRenderer.sharedMaterial == null || 
            trailRenderer.sharedMaterial.name == "Default-Line")
        {
            trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }
        
        // Rendering settings
        trailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        trailRenderer.receiveShadows = false;
        trailRenderer.allowOcclusionWhenDynamic = false;
        
        lastStartColor = startColor;
        lastEndColor = endColor;
    }
    
    void ApplyColors()
    {
        if (trailRenderer == null) return;
        
        // Buat gradient baru dari startColor dan endColor
        Gradient gradient = new Gradient();
        
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(startColor, 0f);
        colorKeys[1] = new GradientColorKey(endColor, 1f);
        
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(startColor.a, 0f);
        alphaKeys[1] = new GradientAlphaKey(endColor.a, 1f);
        
        gradient.SetKeys(colorKeys, alphaKeys);
        
        // PENTING: Set colorGradient, bukan startColor/endColor property!
        trailRenderer.colorGradient = gradient;
    }
    
    /// <summary>
    /// Set warna trail via code
    /// </summary>
    public void SetColors(Color start, Color end)
    {
        startColor = start;
        endColor = end;
        ApplyColors();
    }
    
    /// <summary>
    /// Clear/reset trail
    /// </summary>
    public void ClearTrail()
    {
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }
    }
    
    /// <summary>
    /// Preset warna untuk planet-planet
    /// </summary>
    [ContextMenu("Preset: Earth (Blue)")]
    void PresetEarth()
    {
        startColor = new Color(0.3f, 0.5f, 1f, 1f);
        endColor = new Color(0.3f, 0.5f, 1f, 0f);
        ApplyColors();
    }
    
    [ContextMenu("Preset: Mars (Red)")]
    void PresetMars()
    {
        startColor = new Color(1f, 0.3f, 0.2f, 1f);
        endColor = new Color(1f, 0.3f, 0.2f, 0f);
        ApplyColors();
    }
    
    [ContextMenu("Preset: Venus (Orange)")]
    void PresetVenus()
    {
        startColor = new Color(1f, 0.7f, 0.3f, 1f);
        endColor = new Color(1f, 0.7f, 0.3f, 0f);
        ApplyColors();
    }
    
    [ContextMenu("Preset: Mercury (Gray)")]
    void PresetMercury()
    {
        startColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        endColor = new Color(0.7f, 0.7f, 0.7f, 0f);
        ApplyColors();
    }
    
    [ContextMenu("Preset: Moon (White)")]
    void PresetMoon()
    {
        startColor = new Color(0.9f, 0.9f, 0.9f, 1f);
        endColor = new Color(0.9f, 0.9f, 0.9f, 0f);
        ApplyColors();
    }
}

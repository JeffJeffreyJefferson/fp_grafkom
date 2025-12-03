using UnityEngine;

/// <summary>
/// Script untuk mengatur efek emission pada Matahari
/// dan menambahkan Point Light sebagai sumber cahaya utama
/// </summary>
public class SunEmission : MonoBehaviour
{
    [Header("Emission Settings")]
    public Color emissionColor = new Color(1f, 0.9f, 0.5f);  // Warna emission
    [Range(0f, 10f)]
    public float emissionIntensity = 2f;     // Intensitas emission
    public bool pulseEmission = true;        // Efek pulse
    public float pulseSpeed = 1f;            // Kecepatan pulse
    public float pulseAmount = 0.3f;         // Jumlah variasi pulse
    
    [Header("Light Settings")]
    public bool createLight = true;          // Buat Point Light otomatis
    public float lightIntensity = 2f;        // Intensitas cahaya
    public float lightRange = 100f;          // Jangkauan cahaya
    public Color lightColor = new Color(1f, 0.95f, 0.8f);
    public LightShadows shadowType = LightShadows.Soft;
    
    [Header("Corona Effect")]
    public bool enableCorona = false;        // Efek corona (opsional)
    public float coronaScale = 1.2f;         // Skala corona
    
    private Material sunMaterial;
    private Light sunLight;
    private float baseEmissionIntensity;

    void Start()
    {
        SetupEmission();
        
        if (createLight)
        {
            SetupLight();
        }
    }
    
    void Update()
    {
        if (pulseEmission && sunMaterial != null)
        {
            UpdatePulse();
        }
    }
    
    void SetupEmission()
    {
        Renderer renderer = GetComponent<Renderer>();
        
        if (renderer != null)
        {
            // Dapatkan material instance
            sunMaterial = renderer.material;
            
            // Enable emission
            sunMaterial.EnableKeyword("_EMISSION");
            
            // Set emission color dengan intensitas
            Color finalColor = emissionColor * emissionIntensity;
            sunMaterial.SetColor("_EmissionColor", finalColor);
            
            // Untuk URP
            if (sunMaterial.HasProperty("_EmissiveColor"))
            {
                sunMaterial.SetColor("_EmissiveColor", finalColor);
            }
            
            baseEmissionIntensity = emissionIntensity;
        }
    }
    
    void SetupLight()
    {
        // Cek apakah sudah ada light
        sunLight = GetComponent<Light>();
        
        if (sunLight == null)
        {
            sunLight = GetComponentInChildren<Light>();
        }
        
        if (sunLight == null)
        {
            // Buat light baru
            GameObject lightObj = new GameObject("Sun Light");
            lightObj.transform.SetParent(transform);
            lightObj.transform.localPosition = Vector3.zero;
            sunLight = lightObj.AddComponent<Light>();
        }
        
        // Setup light properties
        sunLight.type = LightType.Point;
        sunLight.color = lightColor;
        sunLight.intensity = lightIntensity;
        sunLight.range = lightRange;
        sunLight.shadows = shadowType;
    }
    
    void UpdatePulse()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        float currentIntensity = baseEmissionIntensity + pulse;
        
        Color finalColor = emissionColor * currentIntensity;
        sunMaterial.SetColor("_EmissionColor", finalColor);
        
        if (sunMaterial.HasProperty("_EmissiveColor"))
        {
            sunMaterial.SetColor("_EmissiveColor", finalColor);
        }
        
        // Update light intensity juga
        if (sunLight != null)
        {
            sunLight.intensity = lightIntensity + (pulse * 0.5f);
        }
    }
    
    /// <summary>
    /// Set emission intensity
    /// </summary>
    public void SetEmissionIntensity(float intensity)
    {
        emissionIntensity = intensity;
        baseEmissionIntensity = intensity;
        
        if (sunMaterial != null)
        {
            Color finalColor = emissionColor * emissionIntensity;
            sunMaterial.SetColor("_EmissionColor", finalColor);
        }
    }
    
    /// <summary>
    /// Set light intensity
    /// </summary>
    public void SetLightIntensity(float intensity)
    {
        lightIntensity = intensity;
        if (sunLight != null)
        {
            sunLight.intensity = intensity;
        }
    }
    
    /// <summary>
    /// Toggle pulse effect
    /// </summary>
    public void TogglePulse(bool enabled)
    {
        pulseEmission = enabled;
    }
}

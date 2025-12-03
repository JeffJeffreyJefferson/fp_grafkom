using UnityEngine;

/// <summary>
/// Script utama untuk mengontrol planet:
/// - Rotasi pada poros sendiri (self-rotation)
/// - Rotasi mengelilingi matahari (orbit)
/// - Dapat dikontrol kecepatannya via SpeedController
/// </summary>
public class PlanetController : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform orbitCenter;           // Target orbit (Matahari)
    public float orbitSpeed = 30f;          // Kecepatan orbit default
    public Vector3 orbitAxis = Vector3.up;  // Sumbu orbit
    
    [Header("Self Rotation Settings")]
    public float selfRotationSpeed = 50f;   // Kecepatan rotasi pada poros
    public Vector3 rotationAxis = Vector3.up; // Sumbu rotasi diri
    
    [Header("Speed Multiplier")]
    [Range(0.1f, 10f)]
    public float speedMultiplier = 1f;      // Pengali kecepatan dari UI
    
    private float baseOrbitSpeed;
    private float baseSelfRotationSpeed;

    void Start()
    {
        // Simpan kecepatan dasar
        baseOrbitSpeed = orbitSpeed;
        baseSelfRotationSpeed = selfRotationSpeed;
        
        if (orbitCenter == null)
        {
            Debug.LogWarning($"{gameObject.name}: Orbit center tidak diset!");
        }
    }

    void Update()
    {
        // Update kecepatan berdasarkan multiplier
        orbitSpeed = baseOrbitSpeed * speedMultiplier;
        selfRotationSpeed = baseSelfRotationSpeed * speedMultiplier;
        
        // Rotasi pada poros sendiri
        transform.Rotate(rotationAxis, selfRotationSpeed * Time.deltaTime, Space.Self);
        
        // Rotasi mengelilingi matahari
        if (orbitCenter != null)
        {
            transform.RotateAround(orbitCenter.position, orbitAxis, orbitSpeed * Time.deltaTime);
        }
    }
    
    /// <summary>
    /// Set speed multiplier dari UI Slider
    /// </summary>
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}

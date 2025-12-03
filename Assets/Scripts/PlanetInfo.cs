using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script untuk menampilkan informasi planet saat di-klik
/// Tambahan fitur edukatif untuk solar system
/// </summary>
public class PlanetInfo : MonoBehaviour
{
    [Header("Planet Information")]
    public string planetName = "Planet";
    [TextArea(3, 5)]
    public string description = "Deskripsi planet";
    public float diameter = 12742f;         // Diameter dalam km
    public float distanceFromSun = 149.6f;  // Jarak dari matahari dalam juta km
    public float orbitalPeriod = 365.25f;   // Periode orbit dalam hari
    public float rotationPeriod = 24f;      // Periode rotasi dalam jam
    public int numberOfMoons = 1;           // Jumlah bulan
    
    [Header("UI References")]
    public GameObject infoPanel;            // Panel UI untuk info
    public TextMeshProUGUI infoText;        // Text untuk info (TMP)
    public Text infoTextLegacy;             // Text untuk info (Legacy)
    public TextMeshProUGUI titleText;       // Text untuk judul
    public Text titleTextLegacy;            // Text untuk judul (Legacy)
    
    [Header("Settings")]
    public bool showOnHover = false;        // Tampilkan saat hover
    public bool showOnClick = true;         // Tampilkan saat klik
    
    private static PlanetInfo currentlyShowing;

    void Start()
    {
        // Sembunyikan panel di awal
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }
    
    void OnMouseDown()
    {
        if (showOnClick)
        {
            ShowInfo();
        }
    }
    
    void OnMouseEnter()
    {
        if (showOnHover)
        {
            ShowInfo();
        }
    }
    
    void OnMouseExit()
    {
        if (showOnHover)
        {
            HideInfo();
        }
    }
    
    /// <summary>
    /// Tampilkan informasi planet
    /// </summary>
    public void ShowInfo()
    {
        // Sembunyikan info planet lain yang sedang tampil
        if (currentlyShowing != null && currentlyShowing != this)
        {
            currentlyShowing.HideInfo();
        }
        
        currentlyShowing = this;
        
        string info = GenerateInfoText();
        
        // Update UI
        if (titleText != null)
        {
            titleText.text = planetName;
        }
        if (titleTextLegacy != null)
        {
            titleTextLegacy.text = planetName;
        }
        
        if (infoText != null)
        {
            infoText.text = info;
        }
        if (infoTextLegacy != null)
        {
            infoTextLegacy.text = info;
        }
        
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }
    }
    
    /// <summary>
    /// Sembunyikan informasi planet
    /// </summary>
    public void HideInfo()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
        
        if (currentlyShowing == this)
        {
            currentlyShowing = null;
        }
    }
    
    /// <summary>
    /// Generate text informasi planet
    /// </summary>
    string GenerateInfoText()
    {
        return $"{description}\n\n" +
               $"Diameter: {diameter:N0} km\n" +
               $"Jarak dari Matahari: {distanceFromSun:N1} juta km\n" +
               $"Periode Orbit: {orbitalPeriod:N1} hari\n" +
               $"Periode Rotasi: {rotationPeriod:N1} jam\n" +
               $"Jumlah Bulan: {numberOfMoons}";
    }
    
    /// <summary>
    /// Toggle info panel
    /// </summary>
    public void ToggleInfo()
    {
        if (infoPanel != null && infoPanel.activeSelf)
        {
            HideInfo();
        }
        else
        {
            ShowInfo();
        }
    }
}

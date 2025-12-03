using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// SCRIPT SETUP OTOMATIS - Jalankan sekali untuk setup semua komponen!
/// 
/// Cara pakai:
/// 1. Buat Empty GameObject, rename "GameManager"
/// 2. Add Component → AutoSetup
/// 3. Klik tombol "Setup All" di Inspector (atau Play game)
/// 4. Setelah setup selesai, bisa hapus script ini
/// </summary>
public class AutoSetup : MonoBehaviour
{
    [Header("Setup Options")]
    public bool setupOnStart = true;
    public bool createSpeedSlider = true;
    public bool createTimeController = true;
    public bool setupCamera = true;
    public bool setupAudio = true;
    
    [Header("References (Auto-detected)")]
    public Transform sun;
    public AudioClip ambienceClip;
    
    void Start()
    {
        if (setupOnStart)
        {
            SetupAll();
        }
    }
    
    [ContextMenu("Setup All")]
    public void SetupAll()
    {
        Debug.Log("=== AUTO SETUP DIMULAI ===");
        
        // 1. Setup EventSystem
        SetupEventSystem();
        
        // 2. Setup Canvas dan UI
        if (createSpeedSlider)
        {
            SetupSpeedSliderUI();
        }
        
        // 3. Setup TimeController
        if (createTimeController)
        {
            SetupTimeController();
        }
        
        // 4. Setup Camera
        if (setupCamera)
        {
            SetupCameraController();
        }
        
        // 5. Setup Audio
        if (setupAudio)
        {
            SetupAudioAmbience();
        }
        
        Debug.Log("=== AUTO SETUP SELESAI ===");
        Debug.Log("Anda bisa hapus script AutoSetup ini sekarang.");
    }
    
    void SetupEventSystem()
    {
        if (FindAnyObjectByType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
            Debug.Log("[AutoSetup] EventSystem dibuat");
        }
        else
        {
            Debug.Log("[AutoSetup] EventSystem sudah ada");
        }
    }
    
    void SetupSpeedSliderUI()
    {
        // Cek apakah Canvas sudah ada
        Canvas canvas = FindAnyObjectByType<Canvas>();
        
        if (canvas == null)
        {
            // Buat Canvas baru
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            Debug.Log("[AutoSetup] Canvas dibuat");
        }
        
        // Pastikan Canvas punya GraphicRaycaster
        if (canvas.GetComponent<GraphicRaycaster>() == null)
        {
            canvas.gameObject.AddComponent<GraphicRaycaster>();
        }
        
        // Cek apakah Slider sudah ada
        Slider existingSlider = FindAnyObjectByType<Slider>();
        
        if (existingSlider == null)
        {
            // Buat Slider
            GameObject sliderObj = new GameObject("SpeedSlider");
            sliderObj.transform.SetParent(canvas.transform, false);
            
            // Background
            GameObject background = new GameObject("Background");
            background.transform.SetParent(sliderObj.transform, false);
            Image bgImage = background.AddComponent<Image>();
            bgImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
            
            // Fill Area
            GameObject fillArea = new GameObject("Fill Area");
            fillArea.transform.SetParent(sliderObj.transform, false);
            RectTransform fillAreaRect = fillArea.AddComponent<RectTransform>();
            
            GameObject fill = new GameObject("Fill");
            fill.transform.SetParent(fillArea.transform, false);
            Image fillImage = fill.AddComponent<Image>();
            fillImage.color = new Color(0.3f, 0.7f, 1f, 1f);
            
            // Handle
            GameObject handleArea = new GameObject("Handle Slide Area");
            handleArea.transform.SetParent(sliderObj.transform, false);
            handleArea.AddComponent<RectTransform>();
            
            GameObject handle = new GameObject("Handle");
            handle.transform.SetParent(handleArea.transform, false);
            Image handleImage = handle.AddComponent<Image>();
            handleImage.color = Color.white;
            
            // Slider component
            Slider slider = sliderObj.AddComponent<Slider>();
            slider.fillRect = fill.GetComponent<RectTransform>();
            slider.handleRect = handle.GetComponent<RectTransform>();
            slider.targetGraphic = handleImage;
            
            // Position slider
            RectTransform sliderRect = sliderObj.GetComponent<RectTransform>();
            sliderRect.anchorMin = new Vector2(0, 0);
            sliderRect.anchorMax = new Vector2(0, 0);
            sliderRect.pivot = new Vector2(0, 0);
            sliderRect.anchoredPosition = new Vector2(20, 20);
            sliderRect.sizeDelta = new Vector2(200, 20);
            
            // Setup rects
            RectTransform bgRect = background.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            
            existingSlider = slider;
            Debug.Log("[AutoSetup] Speed Slider dibuat");
        }
        
        // Buat Text untuk speed
        Text speedText = null;
        Text[] texts = FindObjectsByType<Text>(FindObjectsSortMode.None);
        foreach (var t in texts)
        {
            if (t.name.Contains("Speed"))
            {
                speedText = t;
                break;
            }
        }
        
        if (speedText == null)
        {
            GameObject textObj = new GameObject("SpeedText");
            textObj.transform.SetParent(canvas.transform, false);
            speedText = textObj.AddComponent<Text>();
            speedText.text = "Speed: 1.0x";
            speedText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            speedText.fontSize = 18;
            speedText.color = Color.white;
            speedText.alignment = TextAnchor.MiddleLeft;
            
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0, 0);
            textRect.anchorMax = new Vector2(0, 0);
            textRect.pivot = new Vector2(0, 0);
            textRect.anchoredPosition = new Vector2(20, 50);
            textRect.sizeDelta = new Vector2(200, 30);
            
            Debug.Log("[AutoSetup] Speed Text dibuat");
        }
        
        // Setup SpeedController
        SpeedController speedController = FindAnyObjectByType<SpeedController>();
        if (speedController == null)
        {
            GameObject scObj = new GameObject("SpeedController");
            speedController = scObj.AddComponent<SpeedController>();
        }
        
        speedController.speedSlider = existingSlider;
        speedController.speedText = speedText;
        
        Debug.Log("[AutoSetup] SpeedController configured");
    }
    
    void SetupTimeController()
    {
        TimeController tc = FindAnyObjectByType<TimeController>();
        if (tc == null)
        {
            GameObject tcObj = new GameObject("TimeController");
            tc = tcObj.AddComponent<TimeController>();
            Debug.Log("[AutoSetup] TimeController dibuat");
        }
    }
    
    void SetupCameraController()
    {
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            CameraController cc = mainCam.GetComponent<CameraController>();
            if (cc == null)
            {
                cc = mainCam.gameObject.AddComponent<CameraController>();
                Debug.Log("[AutoSetup] CameraController ditambahkan ke Main Camera");
            }
            
            // Set orbit target ke Sun jika ada
            if (sun == null)
            {
                // Coba cari Sun
                GameObject sunObj = GameObject.Find("Sun");
                if (sunObj != null)
                {
                    sun = sunObj.transform;
                }
            }
            
            if (sun != null)
            {
                cc.orbitTarget = sun;
            }
        }
    }
    
    void SetupAudioAmbience()
    {
        SpaceAmbience sa = FindAnyObjectByType<SpaceAmbience>();
        if (sa == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                sa = mainCam.gameObject.AddComponent<SpaceAmbience>();
                sa.ambienceClip = ambienceClip;
                Debug.Log("[AutoSetup] SpaceAmbience ditambahkan ke Main Camera");
            }
        }
    }
}

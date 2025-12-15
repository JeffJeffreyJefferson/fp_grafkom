using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Daftar Kamera")]
    public GameObject mainCamera; // Masukkan Main Camera (Top View)
    public GameObject bumiCamera; // Masukkan Cam_bumi
    public GameObject marsCamera; // Masukkan Cam_mars (jika ada, kalau tidak kosongkan saja)

    [Header("Pengaturan Zoom")]
    public float zoomSpeed = 20f; // Semakin besar angka, semakin cepat zoom-nya

    void Start()
    {
        // Saat game dimulai, pastikan kita melihat dari kamera utama dulu
        ShowMainView();
    }

    void Update()
    {
        // ==========================================
        // 1. LOGIKA GANTI KAMERA (Keyboard 1, 2, 3)
        // ==========================================
        
        // Tekan Angka 1 untuk Main View (Atas)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowMainView();
        }

        // Tekan Angka 2 untuk View Satelit Bumi
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShowBumiView();
        }

        // Tekan Angka 3 untuk View Mars (Opsional)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShowMarsView();
        }

        // ==========================================
        // 2. LOGIKA ZOOM (Scroll Mouse)
        // ==========================================
        
        // Fitur Zoom hanya aktif jika kita sedang memakai Main Camera
        if (mainCamera.activeSelf)
        {
            // Deteksi putaran roda mouse (Scroll Wheel)
            // Nilainya: Positif (Maju) atau Negatif (Mundur)
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0)
            {
                // Gerakkan kamera Maju/Mundur (Vector3.forward)
                mainCamera.transform.Translate(Vector3.forward * scroll * zoomSpeed, Space.Self);
            }
        }
    }

    // --- FUNGSI-FUNGSI PENGATUR ON/OFF KAMERA ---

    void ShowMainView()
    {
        mainCamera.SetActive(true);
        if (bumiCamera != null) bumiCamera.SetActive(false);
        if (marsCamera != null) marsCamera.SetActive(false);
    }

    void ShowBumiView()
    {
        // Pastikan slot tidak kosong sebelum dinyalakan
        if (bumiCamera != null)
        {
            mainCamera.SetActive(false);
            bumiCamera.SetActive(true);
            if (marsCamera != null) marsCamera.SetActive(false);
        }
    }

    void ShowMarsView()
    {
        // Pastikan slot tidak kosong sebelum dinyalakan
        if (marsCamera != null)
        {
            mainCamera.SetActive(false);
            if (bumiCamera != null) bumiCamera.SetActive(false);
            marsCamera.SetActive(true);
        }
    }
}
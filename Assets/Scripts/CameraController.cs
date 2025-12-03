using UnityEngine;

/// <summary>
/// Script untuk mengontrol kamera dengan zoom, pan, dan orbit
/// BEKERJA OUT OF THE BOX! Tidak perlu setup apapun.
/// 
/// Controls:
/// - Scroll Mouse: Zoom in/out
/// - Klik Kanan + Drag: Pan kamera
/// - Klik Tengah + Drag: Orbit/Rotate kamera
/// - Klik Kiri pada planet: Fokus ke planet (jika ada ChangeLookAtTarget)
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float zoomSpeed = 20f;
    public float minFOV = 10f;
    public float maxFOV = 100f;
    
    [Header("Pan Settings")]
    public float panSpeed = 0.5f;
    public bool enablePan = true;
    
    [Header("Orbit Settings")]
    public float orbitSpeed = 5f;
    public bool enableOrbit = true;
    public Transform orbitTarget;  // Target untuk orbit, bisa Sun
    
    [Header("Debug")]
    public bool showControls = true;
    
    private Camera cam;
    private Vector3 lastMousePosition;
    private float currentFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            cam = Camera.main;
        }
        
        if (cam != null)
        {
            currentFOV = cam.fieldOfView;
        }
        
        if (showControls)
        {
            Debug.Log("[CameraController] Ready! Controls:");
            Debug.Log("  Scroll = Zoom");
            Debug.Log("  Right Click + Drag = Pan");
            Debug.Log("  Middle Click + Drag = Orbit");
        }
    }

    void Update()
    {
        if (cam == null) return;
        
        HandleZoom();
        HandlePan();
        HandleOrbit();
    }
    
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (scroll != 0)
        {
            currentFOV -= scroll * zoomSpeed;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            cam.fieldOfView = currentFOV;
        }
    }
    
    void HandlePan()
    {
        if (!enablePan) return;
        
        // Pan dengan klik kanan
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }
        
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * panSpeed * Time.unscaledDeltaTime;
            transform.Translate(move, Space.Self);
            lastMousePosition = Input.mousePosition;
        }
    }
    
    void HandleOrbit()
    {
        if (!enableOrbit) return;
        
        // Orbit dengan klik tengah
        if (Input.GetMouseButton(2))
        {
            float rotX = Input.GetAxis("Mouse X") * orbitSpeed;
            float rotY = Input.GetAxis("Mouse Y") * orbitSpeed;
            
            if (orbitTarget != null)
            {
                // Orbit around target
                transform.RotateAround(orbitTarget.position, Vector3.up, rotX);
                transform.RotateAround(orbitTarget.position, transform.right, -rotY);
                transform.LookAt(orbitTarget);
            }
            else
            {
                // Rotate in place
                transform.Rotate(Vector3.up, rotX, Space.World);
                transform.Rotate(Vector3.right, -rotY, Space.Self);
            }
        }
    }
    
    /// <summary>
    /// Fokus kamera ke target tertentu
    /// </summary>
    public void FocusOn(Transform target, float distance = 20f)
    {
        if (target == null) return;
        
        Vector3 direction = (transform.position - target.position).normalized;
        if (direction == Vector3.zero) direction = -Vector3.forward;
        
        transform.position = target.position + direction * distance;
        transform.LookAt(target);
        orbitTarget = target;
    }
    
    /// <summary>
    /// Reset zoom ke default
    /// </summary>
    public void ResetZoom()
    {
        currentFOV = 60f;
        if (cam != null)
        {
            cam.fieldOfView = currentFOV;
        }
    }
    
    void OnGUI()
    {
        if (showControls)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 12;
            style.normal.textColor = new Color(1, 1, 1, 0.7f);
            
            GUI.Label(new Rect(10, Screen.height - 60, 300, 20), "Scroll=Zoom | RightClick=Pan | MiddleClick=Orbit", style);
        }
    }
}

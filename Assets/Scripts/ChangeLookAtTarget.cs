using UnityEngine;

public class ChangeLookAtTarget : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        if (target == null)
        {
            target = transform;
            Debug.LogWarning("ChangeLookAtTarget: target tidak diset. Menggunakan transform sendiri.");
        }
    }

    private void OnMouseDown()
    {
        Camera cam = Camera.main;

        if (cam != null)
        {
            LookAtTarget look = cam.GetComponent<LookAtTarget>();
            if (look != null)
            {
                look.target = target;
            }

            float fov = Mathf.Clamp(60f * target.localScale.x, 1f, 100f);
            cam.fieldOfView = fov;
        }
        else
        {
            Debug.LogError("Tidak ada Camera dengan tag 'MainCamera'.");
        }
    }
}

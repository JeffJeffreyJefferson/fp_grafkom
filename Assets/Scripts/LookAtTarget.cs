using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target != null)
            transform.LookAt(target);
    }
}

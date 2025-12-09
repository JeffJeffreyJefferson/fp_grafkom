using UnityEngine;

public class RotateAroundComet : MonoBehaviour
{
    public Transform target; // the object to rotate around (Matahari)
    public float speed; // the speed of rotation
    public Vector3 rotationAxis = new Vector3(1, 0, 1); // Diagonal dari atas kanan ke kiri bawah

    void Start()
    {
        if (target == null)
        {
            target = this.gameObject.transform;
            Debug.Log("RotateAround target not specified. Defaulting to parent GameObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Normalizing the rotation axis so it's a unit vector
        Vector3 normalizedAxis = rotationAxis.normalized;

        // RotateAround takes three arguments: position to rotate around, axis to rotate around, and degrees to rotate
        transform.RotateAround(target.position, normalizedAxis, speed * Time.deltaTime);
    }
}

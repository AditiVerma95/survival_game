using UnityEngine;

public class SmoothYRotation : MonoBehaviour
{
    public float rotationSpeed = 30f; // Degrees per second

    void Update()
    {
        // Rotate around Y axis smoothly
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
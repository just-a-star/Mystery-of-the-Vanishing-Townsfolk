using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign this to your player's transform in the inspector.
    public float smoothSpeed = 0.125f; // Adjust this if you want the camera to follow more or less smoothly.
    public Vector3 offset; // Adjust this if you want the camera to be offset from the player position.

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}

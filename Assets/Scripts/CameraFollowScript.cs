using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}


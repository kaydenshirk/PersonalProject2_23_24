using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothSpeed = 5.0f; // Adjusted smoothness of camera movement
    public Vector3 offset; // Offset to fine-tune the camera position
    public float offsetWhenMovingLeft = 10.0f; // Offset when the camera is moving to the left

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the offset based on the player's movement direction
            float offsetZ;

            if (target.forward.z > 0)
            {
                offsetZ = 0.0f;
            }
            else
            {
                offsetZ = offsetWhenMovingLeft;
            }

            // Calculate the desired position with the offset
            Vector3 desiredPosition = target.position + new Vector3(offset.x, offset.y, offset.z + offsetZ);

            // Use Time.deltaTime in the Lerp function
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Move the camera towards the smoothed position
            transform.position = smoothedPosition;
        }
    }
}

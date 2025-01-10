using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public float followSpeed = 2f; // Speed at which the camera moves toward the player
    public float movementThreshold = 1f; // Distance the player needs to move for the camera to adjust

    private Vector3 offset;        // Offset between the camera and player
    private Vector3 targetPosition; // Smoothed target position

    void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Calculate the target position for the camera based on the player's current position
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate the camera's position to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}


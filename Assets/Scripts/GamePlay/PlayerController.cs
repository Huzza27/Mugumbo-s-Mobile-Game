using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement speed of the player
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD or arrow keys
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        // Normalize the direction to ensure consistent speed in all directions
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Calculate new position
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Update the player's position
        transform.position = newPosition;
    }
}

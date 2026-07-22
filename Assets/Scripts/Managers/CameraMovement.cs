using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX, maxX, minY, maxY;
    public float zoomSpeed,minZoom,maxZoom;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.fixedDeltaTime;

        // Move the camera
        transform.Translate(move);

        // Clamp the camera's position to stay within bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Apply the clamped values
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        
    }

    private void Update()
    {
        // Zoom in/out (change the camera's orthographic size)
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");  // Get zoom input (mouse scroll wheel)
        mainCamera.orthographicSize -= zoomInput * zoomSpeed;

        // Clamp zoom level to prevent the camera from zooming too far in or out
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);

        if (zoomInput != 0)
        {
            ZoomAtMousePosition(zoomInput);
        }
    }

    void ZoomAtMousePosition(float zoomInput)
    {
        // Convert mouse position from screen space to world space
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = -1; // Set Z to 0 since it's a 2D game

        // Calculate the target zoom level (camera orthographic size)
        float targetSize = mainCamera.orthographicSize - zoomInput * zoomSpeed;

        // Clamp zoom level to prevent the camera from zooming too far in or out
        targetSize = Mathf.Clamp(targetSize, 2f, 20f); // Adjust the min and max zoom limits as necessary

        // Move the camera so the mouse stays in the same world position
        Vector3 mouseWorldPositionBeforeZoom = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mainCamera.orthographicSize = targetSize;

        // Calculate the difference in the camera's orthographic size
        Vector3 mouseWorldPositionAfterZoom = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mouseWorldPositionBeforeZoom - mouseWorldPositionAfterZoom;

        // Adjust the camera position so the mouse stays at the same position in world space
        transform.position += diff;
    }

    // Visualize the camera's movement bounds using Gizmos
    void OnDrawGizmos()
    {
        // Set the Gizmos color to something visible
        Gizmos.color = Color.red;

        // Draw a rectangle to represent the camera's movement bounds
        Vector3 topLeft = new Vector3(minX, maxY, 0);
        Vector3 topRight = new Vector3(maxX, maxY, 0);
        Vector3 bottomLeft = new Vector3(minX, minY, 0);
        Vector3 bottomRight = new Vector3(maxX, minY, 0);

        // Draw the boundaries as a wireframe rectangle
        Gizmos.DrawLine(topLeft, topRight);  // Top edge
        Gizmos.DrawLine(topRight, bottomRight);  // Right edge
        Gizmos.DrawLine(bottomRight, bottomLeft);  // Bottom edge
        Gizmos.DrawLine(bottomLeft, topLeft);  // Left edge

        // Optionally, you can draw a small sphere or marker to show the clamped position
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
        //                                  Mathf.Clamp(transform.position.y, minY, maxY), 0), 0.5f);
    }
}

using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GridManager gridManager; // Reference to the GridManager
    public float moveSpeed = 2f; // Speed of the player movement

    private bool isMoving = false; // Tracks if the player is currently moving
    private Vector3 targetPosition; // The target position to move towards

    private void Update()
    {
        // Check for mouse input only if the player is not moving
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            HandleTileClick();
        }
    }

    private void HandleTileClick()
    {
        // Perform a raycast from the mouse position to detect the clicked grid
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 clickedPosition = hit.point; // Get the world position of the clicked point
            Vector2Int gridCoords = gridManager.GetGridCoordinates(clickedPosition); // Convert to grid coordinates

            // Check if the clicked tile is walkable
            if (gridManager.IsTileWalkable(gridCoords.x, gridCoords.y))
            {
                targetPosition = gridManager.GetTilePosition(gridCoords.x, gridCoords.y); // Get the world position of the target tile
                StartCoroutine(MoveToTarget()); // Start moving to the clicked tile
            }
            else
            {
                Debug.Log("Tile is not walkable!");
            }
        }
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true; // Lock movement input

        // Move the player towards the target position
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Snap to the exact target position
        transform.position = targetPosition;

        isMoving = false; // Unlock movement input after reaching the target
        Debug.Log("Player reached the target position.");
    }
}

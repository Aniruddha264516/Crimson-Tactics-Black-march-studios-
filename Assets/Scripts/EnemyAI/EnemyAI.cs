using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public GridManager gridManager; // Reference to the GridManager
    public PlayerMovement player; // Reference to the player
    public float moveSpeed = 2f; // Speed of movement

    private Vector2Int currentGridPosition; // Current grid position of the enemy
    private bool isMoving = false; // Whether the enemy is currently moving
    private Vector2Int lastPlayerPosition; // Stores the last known player position

    private void Start()
    {
        // Initialize the enemy's starting grid position
        currentGridPosition = gridManager.GetGridCoordinates(transform.position);
        lastPlayerPosition = gridManager.GetGridCoordinates(player.transform.position);
    }

    private void Update()
    {
        // If the enemy is not moving and the player has moved, start moving
        Vector2Int currentPlayerPosition = gridManager.GetGridCoordinates(player.transform.position);
        if (!isMoving && currentPlayerPosition != lastPlayerPosition)
        {
            MoveTowardsPlayer(currentPlayerPosition);
            lastPlayerPosition = currentPlayerPosition; // Update last known position
        }
    }

    public void MoveTowardsPlayer(Vector2Int playerPosition)
    {
        // If already adjacent to the player, stop moving
        if (IsAdjacentToPlayer(playerPosition))
        {
            StopMovement();
            return;
        }

        // Use pathfinding to determine the next tile to move towards
        List<Vector2Int> path = gridManager.FindPath(currentGridPosition, playerPosition);

        if (path.Count > 0)
        {
            Vector2Int nextTile = path[0];
            StartCoroutine(MoveToTile(nextTile));
        }
    }

    private IEnumerator MoveToTile(Vector2Int targetGridPosition)
    {
        isMoving = true;

        Vector3 targetWorldPosition = gridManager.GetTilePosition(targetGridPosition.x, targetGridPosition.y);

        // Move towards the target tile
        while (Vector3.Distance(transform.position, targetWorldPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Snap to the exact position
        transform.position = targetWorldPosition;
        currentGridPosition = targetGridPosition; // Update the current grid position

        isMoving = false; // Movement complete
    }

    public void StopMovement()
    {
        // Do nothing for now; stopping behavior can be expanded if needed
        Debug.Log("Enemy is adjacent to the player and has stopped moving.");
    }

    private bool IsAdjacentToPlayer(Vector2Int playerPosition)
    {
        // Check if the enemy is in one of the 4 adjacent tiles to the player
        return (Mathf.Abs(currentGridPosition.x - playerPosition.x) == 1 && currentGridPosition.y == playerPosition.y) ||
               (Mathf.Abs(currentGridPosition.y - playerPosition.y) == 1 && currentGridPosition.x == playerPosition.x);
    }
}

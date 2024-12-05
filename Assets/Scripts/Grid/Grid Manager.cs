using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;  // Width of the grid
    public int gridHeight = 10; // Height of the grid
    public float cellSize = 1f; // Size of each cell in the grid
    public GameObject tilePrefab; // Optional: Tile visualization prefab

    private Vector3[,] gridWorldPositions; // Stores world positions for each grid cell
    private bool[,] walkableTiles; // Stores walkability information for each tile

    private void Start()
    {
        InitializeGrid();
    }

    // Initialize the grid system
    private void InitializeGrid()
    {
        gridWorldPositions = new Vector3[gridWidth, gridHeight];
        walkableTiles = new bool[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Calculate world position for this grid cell
                gridWorldPositions[x, y] = new Vector3(x * cellSize, 1.5f, y * cellSize);

                // Mark all tiles as walkable by default
                walkableTiles[x, y] = true;

                // Optional: Instantiate tile prefabs for visualization
                if (tilePrefab != null)
                {
                    Instantiate(tilePrefab, gridWorldPositions[x, y], Quaternion.identity, transform);
                }
            }
        }
    }

    // Convert a world position to grid coordinates
    public Vector2Int GetGridCoordinates(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.z / cellSize);

        // Clamp the coordinates to stay within the grid bounds
        x = Mathf.Clamp(x, 0, gridWidth - 1);
        y = Mathf.Clamp(y, 0, gridHeight - 1);

        return new Vector2Int(x, y);
    }

    // Get the world position of a grid tile from its grid coordinates
    public Vector3 GetTilePosition(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return gridWorldPositions[x, y];
        }

        Debug.LogWarning("Grid coordinates out of bounds!");
        return Vector3.zero; // Return a default value if out of bounds
    }

    // Check if a grid tile is walkable
    public bool IsTileWalkable(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return walkableTiles[x, y];
        }

        return false; // Return false for out-of-bounds tiles
    }

    // Set a tile's walkability (walkable or blocked)
    public void SetTileWalkable(int x, int y, bool isWalkable)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            walkableTiles[x, y] = isWalkable;
        }
    }

    // Breadth-First Search (BFS) Pathfinding Algorithm
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        List<Vector2Int> path = new List<Vector2Int>();

        frontier.Enqueue(start);
        cameFrom[start] = start;

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();

            if (current == goal)
            {
                // Reconstruct the path from the goal to the start
                Vector2Int step = goal;
                while (step != start)
                {
                    path.Add(step);
                    step = cameFrom[step];
                }
                path.Reverse();
                break;
            }

            // Get neighbors and add them to the frontier if they're walkable
            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(neighbor) && IsTileWalkable(neighbor.x, neighbor.y))
                {
                    frontier.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        return path;
    }

    // Get neighboring tiles of a given grid position
    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            position + Vector2Int.up,
            position + Vector2Int.down,
            position + Vector2Int.left,
            position + Vector2Int.right
        };

        // Remove neighbors that are out of grid bounds
        neighbors.RemoveAll(n => n.x < 0 || n.x >= gridWidth || n.y < 0 || n.y >= gridHeight);

        return neighbors;
    }
}

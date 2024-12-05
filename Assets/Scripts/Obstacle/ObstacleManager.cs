using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GridObstacleData gridData; // Reference to the ScriptableObject
    public GameObject obstaclePrefab; // Prefab for obstacles (e.g., red sphere)
    public float tileSize = 1f; // Size of each grid tile

    private void Start()
    {
        if (gridData == null)
        {
            Debug.LogError("GridObstacleData is not assigned!");
            return;
        }

        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        // Clear existing obstacles
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Loop through grid and place obstacles
        for (int y = 0; y < gridData.gridHeight; y++)
        {
            for (int x = 0; x < gridData.gridWidth; x++)
            {
                if (gridData.IsObstacle(x, y))
                {
                    Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);
                    Instantiate(obstaclePrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}

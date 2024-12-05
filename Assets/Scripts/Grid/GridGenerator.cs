using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject cubePrefab;  // The prefab of the cube
    public int gridSize = 10;      // Size of the grid (10x10)
    public float spacing = 2.0f;   // Space between the cubes

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Loop to generate a grid of cubes
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                // Calculate the position of each cube based on grid spacing
                Vector3 position = new Vector3(x * spacing, 0, z * spacing);

                // Instantiate a new cube at the correct position
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.name = "Cube_" + x + "_" + z; // Name the cube uniquely

                // Attach the Tile script to the cube
                Tile tile = cube.AddComponent<Tile>();

                // Initialize the tile properties (e.g., set the grid coordinates)
                tile.x = x;
                tile.z = z;
                tile.isBlocked = false;  // Default to unblocked, but can be changed later
                tile.hasUnit = false;    // Default to no unit on the tile
            }
        }
    }
}

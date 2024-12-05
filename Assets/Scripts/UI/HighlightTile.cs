using UnityEngine;

public class HighlightTile : MonoBehaviour
{
    public Camera mainCamera;               // Reference to the main camera
    public Color highlightColor = Color.yellow; // Color to highlight tiles
    private Color originalColor;            // Store the original color of the tile
    private GameObject currentTile;         // The tile currently being hovered over
    private bool[,] disabledTiles;          // Track disabled tiles

    void Start()
    {
        // Initialize the disabled tiles array (10x10 grid for this example)
        disabledTiles = new bool[10, 10]; // Adjust based on the grid size
    }

    void Update()
    {
        // Perform raycast to detect only grid tiles (not the plane)
        RaycastTile();
    }

    void RaycastTile()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Ensure we hit a grid tile (tagged as "GridTile")
            if (hit.collider.CompareTag("GridTile"))
            {
                GameObject tile = hit.collider.gameObject;
                Vector3 tilePosition = tile.transform.position;

                // Only highlight if the tile isn't disabled
                if (!IsTileDisabled(tilePosition))
                {
                    if (tile != currentTile)
                    {
                        RestoreTileColor(currentTile);
                        HighlightTileColor(tile);
                        currentTile = tile;
                    }
                }
            }
        }
        else
        {
            // No tile is hovered, restore previous tile color
            RestoreTileColor(currentTile);
            currentTile = null;
        }
    }

    void HighlightTileColor(GameObject tile)
    {
        if (tile == null) return;

        // Change the color of the tile's material
        Renderer renderer = tile.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color;
            renderer.material.color = highlightColor;
        }
    }

    void RestoreTileColor(GameObject tile)
    {
        if (tile == null) return;

        // Restore the tile's original color
        Renderer renderer = tile.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = originalColor;
        }
    }

    public void DisableForTile(int x, int y)
    {
        // Disable highlighting for this tile
        disabledTiles[x, y] = true;
    }

    public void EnableForTile(int x, int y)
    {
        // Re-enable highlighting for this tile
        disabledTiles[x, y] = false;
    }

    private bool IsTileDisabled(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x); // Assuming position.x corresponds to grid row
        int y = Mathf.FloorToInt(position.z); // Assuming position.z corresponds to grid column

        return disabledTiles[x, y];
    }
}

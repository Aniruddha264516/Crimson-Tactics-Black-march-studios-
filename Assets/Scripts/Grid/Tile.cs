using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;  // The x coordinate of the tile in the grid
    public int z;  // The z coordinate of the tile in the grid
    public bool isBlocked;  // Is the tile blocked or passable (for obstacles, etc.)

    // You can add more properties depending on what information you need for the tile
    // For example, you can store whether the tile has a unit on it.
    public bool hasUnit;

    // Optionally, add a method to display information about the tile (for debugging purposes)
    public void ShowTileInfo()
    {
        Debug.Log("Tile at (" + x + ", " + z + ") - Blocked: " + isBlocked + ", Has Unit: " + hasUnit);
    }
}

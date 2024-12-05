using UnityEngine;
using UnityEngine.UI; // For UI text
using TMPro; // Optional, if you are using TextMeshPro for UI

public class Raycast : MonoBehaviour
{
    public Camera mainCamera;      // The main camera in the scene
    public TextMeshProUGUI infoText; // UI text element to display tile info (if using TextMeshPro)
    // public Text infoText; // If you're using the default Unity Text component instead of TextMeshPro

    void Update()
    {
        RaycastTile();
    }

    void RaycastTile()
    {
        // Create a ray from the mouse position on the screen
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hits a tile (cube)
            Tile tile = hit.collider.GetComponent<Tile>();  // Get the Tile script attached to the hit object
            if (tile != null)
            {
                // Display the tile information
                ShowTileInfo(tile);
            }
        }
    }

    void ShowTileInfo(Tile tile)
    {
        // Format the information to display
        string tileInfo = "Tile Position: (" + tile.x + ", " + tile.z + ")\n";

        if (tile.hasUnit)
        {
            tileInfo += "Unit Present: Yes";
        }
        else
        {
            tileInfo += "Unit Present: No";
        }

        // Update the UI with the tile info
        infoText.text = tileInfo;
    }
}

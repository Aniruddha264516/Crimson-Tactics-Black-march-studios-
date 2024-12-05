using UnityEngine;

[CreateAssetMenu(fileName = "GridObstacleData", menuName = "ScriptableObjects/GridObstacleData", order = 1)]
public class GridObstacleData : ScriptableObject
{
    public int gridWidth = 10;  // Default grid width
    public int gridHeight = 10; // Default grid height
    public bool[] obstacleData; // Stores obstacle states (1D array)

    public void Initialize()
    {
        obstacleData = new bool[gridWidth * gridHeight];
    }

    public bool IsObstacle(int x, int y)
    {
        int index = x + y * gridWidth;
        return obstacleData[index];
    }

    public void SetObstacle(int x, int y, bool isBlocked)
    {
        int index = x + y * gridWidth;
        obstacleData[index] = isBlocked;
    }
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridObstacleData))]
public class GridEditorTool : Editor
{
    private GridObstacleData gridData;

    private void OnEnable()
    {
        gridData = (GridObstacleData)target;

        // Ensure obstacle data is initialized
        if (gridData.obstacleData == null || gridData.obstacleData.Length == 0)
        {
            gridData.Initialize();
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.LabelField("Grid Editor", EditorStyles.boldLabel);

        for (int y = 0; y < gridData.gridHeight; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < gridData.gridWidth; x++)
            {
                bool isBlocked = gridData.IsObstacle(x, y);
                bool toggled = GUILayout.Toggle(isBlocked, GUIContent.none, GUILayout.Width(20), GUILayout.Height(20));

                if (toggled != isBlocked)
                {
                    gridData.SetObstacle(x, y, toggled);
                    EditorUtility.SetDirty(gridData); // Mark ScriptableObject as modified
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    bool gizmoEnabled;
    GridManager manager = null;

    static bool[] showFloor;

    public static bool showGrid;

    public static List<Node> nodeViz = new List<Node>();
    static int showItemIdx = 0;
    string[] showItemOptions = new string[]
    {
     "Path only", "Box only", "Full","None"
    };

    static int showWalkableIdx = 0;
    string[] showWalkableOptions = new string[]
    {
     "Walkable only", "Obstacles only", "All","None"
    };

    // Add menu named "LevelEditor" to the Window menu
    [MenuItem("Window/LevelEditor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelEditor window = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        window.manager.Init();
        window.manager.CheckObstacles();
        window.Show();
    }

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Active)]
    static void DrawGizmoForMyScript(GridManager scr, GizmoType gizmoType)
    {
        if (showGrid)
        {
            Gizmos.color = Color.black;
            for (int x = 0; x < scr.XLength; x++)
            {
                for (int y = 0; y < scr.YLength; y++)
                {
                    for (int z = 0; z < scr.ZLength; z++)
                    {
                        Vector3 tp = scr.minPos + new Vector3(x * scr.xzScale + .5f, y * scr.yScale, z * scr.xzScale + .5f);
                        if (showFloor[y])
                            Gizmos.DrawWireCube(tp, new Vector3(1, 0.01f, 1));
                    }
                }
            }
        }

        if (showItemIdx == 3) return;

        for (int i = 0; i < nodeViz.Count; i++)
        {
            if (showItemIdx == 1 || showItemIdx == 2)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(nodeViz[i].worldPosition, scr.extends);
            }

            if (showItemIdx == 0 || showItemIdx == 2)
            {
                Gizmos.color = Color.blue;
                if (!nodeViz[i].wallForward)
                    Gizmos
                        .DrawLine(nodeViz[i].worldPosition,
                        nodeViz[i].worldPosition + Vector3.forward / 2);

                if (!nodeViz[i].wallBack)
                    Gizmos
                        .DrawLine(nodeViz[i].worldPosition,
                        nodeViz[i].worldPosition + Vector3.back / 2);
                if (!nodeViz[i].wallRight)
                    Gizmos
                        .DrawLine(nodeViz[i].worldPosition,
                        nodeViz[i].worldPosition + Vector3.right / 2);

                if (!nodeViz[i].wallLeft)
                    Gizmos
                        .DrawLine(nodeViz[i].worldPosition,
                        nodeViz[i].worldPosition + Vector3.left / 2);

                if (!nodeViz[i].wallTop)
                    Gizmos
                        .DrawLine(nodeViz[i].worldPosition,
                        nodeViz[i].worldPosition + Vector3.up / 2);
            }
        }
    }

    private void OnDestroy()
    {
        showGrid = false;
        nodeViz.Clear();
        manager.Reset();
        manager = null;
    }

    private void Awake()
    {
        GridManager[] managers = GameObject.FindObjectsOfType<GridManager>();
        manager = managers[0];
    }

    private bool CheckIsWalkable(Node node)
    {
        if ((showWalkableIdx == 0 || showWalkableIdx == 2) && node.isWalkable)
        {
            return true;
        }
        return false;
    }

    private bool CheckObstacle(Node node)
    {
        if ((showWalkableIdx == 1 || showWalkableIdx == 2) && node.obstacle != null)
        {
            return true;
        }
        return false;
    }

    private bool CheckFloor(Node node)
    {
        if (showFloor[node.y])
        {
            return true;
        }
        return false;
    }

    private void UpdateViz()
    {
        nodeViz.Clear();
        int startY =
            Mathf.FloorToInt(manager.minPos.y) >= 0
                ? Mathf.FloorToInt(manager.minPos.y)
                : 0;
        int startX =
            Mathf.FloorToInt(manager.minPos.x) >= 0
                ? Mathf.FloorToInt(manager.minPos.x)
                : 0;
        int startZ =
            Mathf.FloorToInt(manager.minPos.z) >= 0
                ? Mathf.FloorToInt(manager.minPos.z)
                : 0;

        for (int y = startY; y < manager.YLength; y++)
        {
            for (int x = startX; x < manager.XLength; x++)
            {
                for (int z = startZ; z < manager.ZLength; z++)
                {
                    manager.CheckObstacles(x, y, z);
                    if (manager.grid == null) return;
                    Node node = manager.grid[x, y, z];

                    bool shouldShow = false;
                    shouldShow = CheckIsWalkable(node) ? true : shouldShow;
                    shouldShow = CheckObstacle(node) ? true : shouldShow;
                    shouldShow = !CheckFloor(node) ? false : shouldShow;

                    if (shouldShow)
                    {
                        nodeViz.Add(node);
                        continue;
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (gizmoEnabled)
        {
            UpdateViz();
        }
    }

    void OnGUI()
    {
        if (manager != null && manager.grid != null)
        {
            if (GUILayout.Button("Update Grid"))
            {
                UpdateViz();
            }

            gizmoEnabled = EditorGUILayout.Toggle("Auto refresh", gizmoEnabled);
            showGrid = EditorGUILayout.Toggle("Show grid", showGrid);

            int gridHeight = manager.grid.GetLength(1);
            if (manager.grid != null && gridHeight > 0)
            {
                if (showFloor == null || showFloor.Length != gridHeight)
                    showFloor = new bool[gridHeight];

                for (int i = 0; i < gridHeight; i++)
                {
                    showFloor[i] = EditorGUILayout.Toggle("Show floor " + i, showFloor[i]);
                }
            }
            showWalkableIdx = EditorGUILayout.Popup("Display walkables/obstacles", showWalkableIdx, showWalkableOptions);
            showItemIdx = EditorGUILayout.Popup("Show items", showItemIdx, showItemOptions);
        }
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    string myString = "LevelEditor";

    bool gizmoEnabled;

    bool myBool = true;

    float myFloat = 1.23f;

    GridManager manager = null;

    bool[] showFloor;

    bool showIsWalkable;

    bool showObstacle;

    public static bool showPath;

    public static bool showBox;

    public static List<Node> nodeViz = new List<Node>();

    // Add menu named "LevelEditor" to the Window menu
    [MenuItem("Window/LevelEditor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow) EditorWindow.GetWindow(typeof (MyWindow));
        window.manager.Init();
        window.manager.CheckObstacles();
        window.Show();
    }

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Active)]
    static void DrawGizmoForMyScript(GridManager scr, GizmoType gizmoType)
    {
        for (int i = 0; i < nodeViz.Count; i++)
        {
            if (showBox)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(nodeViz[i].worldPosition, scr.extends);
            }

            if (showPath)
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
        if (showIsWalkable && node.isWalkable)
        {
            return true;
        }
        return false;
    }

    private bool CheckObstacle(Node node)
    {
        if (showObstacle && node.obstacle != null)
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
                    manager.CheckObstacles (x, y, z);
                    Node node = manager.grid[x, y, z];

                    bool shouldShow = false;
                    shouldShow = CheckIsWalkable(node) ? true : shouldShow;
                    shouldShow = CheckObstacle(node) ? true : shouldShow;
                    shouldShow = !CheckFloor(node) ? false : shouldShow;

                    if (shouldShow)
                    {
                        nodeViz.Add (node);
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

            int gridHeight = manager.grid.GetLength(1);
            if (manager.grid != null && gridHeight > 0)
            {
                if (showFloor == null || showFloor.Length != gridHeight)
                    showFloor = new bool[gridHeight];

                for (int i = 0; i < gridHeight; i++)
                {
                    showFloor[i] =
                        EditorGUILayout.Toggle("Show floor " + i, showFloor[i]);
                }
            }
            showIsWalkable =
                EditorGUILayout.Toggle("Show walkable", showIsWalkable);
            showObstacle =
                EditorGUILayout.Toggle("Show Obstacles", showObstacle);
            showPath = EditorGUILayout.Toggle("Show Path", showPath);
            showBox = EditorGUILayout.Toggle("Show Box", showBox);
        }
    }
}

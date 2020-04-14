﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
#region Variables
    public Node[,,] grid;

    [SerializeField]
    float xzScale = 1.5f;

    [SerializeField]
    float yScale = 2;

    public Vector3 minPos;

    public int maxX;

    public int maxY;

    public int maxZ;

    public int XLength;

    public int YLength;

    public int ZLength;

    public Vector3 extends = new Vector3(0.8f, 0.8f, 0.8f);


#endregion


    bool[] floorsToShow;

    public void Reset()
    {
        grid = null;
    }

    public void Init()
    {
        ReadLevel();
    }

    void ReadLevel()
    {
        GridPosition[] gp = GameObject.FindObjectsOfType<GridPosition>();

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;
        float minZ = float.MaxValue;
        float maxZ = float.MinValue;

        for (int i = 0; i < gp.Length; i++)
        {
            Transform t = gp[i].transform;


#region Read Positions
            if (t.position.x < minX)
            {
                minX = t.position.x;
            }

            if (t.position.x > maxX)
            {
                maxX = t.position.x;
            }

            if (t.position.z < minZ)
            {
                minZ = t.position.z;
            }

            if (t.position.z > maxZ)
            {
                maxZ = t.position.z;
            }

            if (t.position.y < minY)
            {
                minY = t.position.y;
            }

            if (t.position.y > maxY)
            {
                maxY = t.position.y;
            }
#endregion
        }

        XLength = Mathf.FloorToInt((maxX - minX) / xzScale);
        YLength = Mathf.FloorToInt((maxY - minY) / yScale);
        ZLength = Mathf.FloorToInt((maxZ - minZ) / xzScale);

        if (YLength == 0)
        {
            YLength = 1;
        }

        minPos = Vector3.zero;
        minPos.x = minX;
        minPos.z = minZ;
        minPos.y = minY;

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[XLength, YLength, ZLength];

        int startY =
            Mathf.FloorToInt(minPos.y) >= 0 ? Mathf.FloorToInt(minPos.y) : 0;
        int startX =
            Mathf.FloorToInt(minPos.x) >= 0 ? Mathf.FloorToInt(minPos.x) : 0;
        int startZ =
            Mathf.FloorToInt(minPos.z) >= 0 ? Mathf.FloorToInt(minPos.z) : 0;
        for (int y = startY; y < YLength; y++)
        {
            for (int x = startX; x < XLength; x++)
            {
                for (int z = startZ; z < ZLength; z++)
                {
                    Node n = new Node();
                    n.x = x;
                    n.z = z;
                    n.y = y;

                    Vector3 tp = minPos;
                    tp.x += x * xzScale + .5f;
                    tp.z += z * xzScale + .5f;
                    tp.y += y * yScale + .5f;
                    n.worldPosition = tp;

                    grid[x, y, z] = n;
                }
            }
        }
    }

    public void CheckObstacles()
    {
        int startY =
            Mathf.FloorToInt(minPos.y) >= 0 ? Mathf.FloorToInt(minPos.y) : 0;
        int startX =
            Mathf.FloorToInt(minPos.x) >= 0 ? Mathf.FloorToInt(minPos.x) : 0;
        int startZ =
            Mathf.FloorToInt(minPos.z) >= 0 ? Mathf.FloorToInt(minPos.z) : 0;
        for (int y = startY; y < YLength; y++)
        {
            for (int x = startX; x < XLength; x++)
            {
                for (int z = startZ; z < ZLength; z++)
                {
                    CheckObstacles (x, y, z);
                }
            }
        }
    }

    public void CheckObstacles(int x, int y, int z)
    {
        Node n = grid[x, y, z];
        n.obstacle = null;
        Collider[] overlapNode =
            Physics
                .OverlapBox(n.worldPosition, extends / 2, Quaternion.identity);

        if (overlapNode.Length > 0)
        {
            bool isWalkable = false;
            for (int i = 0; i < overlapNode.Length; i++)
            {
                GridObject obj =
                    overlapNode[i]
                        .transform
                        .GetComponentInChildren<GridObject>();

                if (obj != null)
                {
                    if (obj.isWalkable && n.obstacle == null)
                    {
                        isWalkable = true;
                    }
                    else
                    {
                        isWalkable = false;
                        n.obstacle = obj;
                    }
                }
            }
            n.isWalkable = isWalkable;
        }
        CheckForWalls (x, y, z);
    }

    private void CheckForWalls(int x, int y, int z)
    {
        Node node = grid[x, y, z];
        node.wallForward = null;
        node.wallBack = null;
        node.wallLeft = null;
        node.wallRight = null;
        node.wallTop = null;

        RaycastHit hit;
        if (Physics.Raycast(node.worldPosition, Vector3.forward, out hit, .6f))
        {
            node.wallForward =
                hit.collider.gameObject.GetComponent<GridObject>();
        }

        if (Physics.Raycast(node.worldPosition, Vector3.back, out hit, .6f))
        {
            node.wallBack = hit.collider.gameObject.GetComponent<GridObject>();
        }

        if (Physics.Raycast(node.worldPosition, Vector3.right, out hit, .6f))
        {
            node.wallRight = hit.collider.gameObject.GetComponent<GridObject>();
        }

        if (Physics.Raycast(node.worldPosition, Vector3.left, out hit, .6f))
        {
            node.wallLeft = hit.collider.gameObject.GetComponent<GridObject>();
        }

        if (Physics.Raycast(node.worldPosition, Vector3.up, out hit, .6f))
        {
            node.wallTop = hit.collider.gameObject.GetComponent<GridObject>();
        }
    }
}

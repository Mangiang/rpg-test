using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    int turnIndex;

    public Turn[] turns;

    public GridManager gridManager;

    bool isInit;

    public float delta;

    public LineRenderer pathViz;

    bool isPathfinding;

    #region Init
    private void Start()
    {
        gridManager.Init();
        PlaceUnits();
        InitStateManagers();
        isInit = true;
    }

    void InitStateManagers()
    {
        foreach (Turn turn in turns)
        {
            turn.player.Init();
        }
    }

    void PlaceUnits()
    {
        GridCharacter[] units = GameObject.FindObjectsOfType<GridCharacter>();
        foreach (GridCharacter unit in units)
        {
            Node n = gridManager.GetNode(unit.transform.position);
            if (n != null)
            {
                unit.OnInit();
                unit.transform.position = n.worldPosition;
                n.character = unit;
                unit.currentNode = n;
            }
        }
    }

    #endregion

    #region Pathfinding Calls
    public void PathfinderCall(GridCharacter gridCharacter, Node targetNode)
    {
        if (!isPathfinding)
        {
            isPathfinding = true;

            Node start = gridCharacter.currentNode;
            if (start != null && targetNode != null)
            {
                PathfinderMaster
                    .singleton
                    .RequestPathfind(gridCharacter,
                    start,
                    targetNode,
                    PathfinderCallback,
                    gridManager);
            }
            else
            {
                isPathfinding = false;
            }
        }
    }

    void PathfinderCallback(List<Node> path, GridCharacter character)
    {
        isPathfinding = false;
        if (path == null) return;

        pathViz.positionCount = path.Count + 1;
        List<Vector3> positions = new List<Vector3>();
        positions.Add(character.currentNode.worldPosition);
        for (int i = 0; i < path.Count; i++)
        {
            positions.Add(path[i].worldPosition - Vector3.up * .5f);
        }

        pathViz.SetPositions(positions.ToArray());
    }

    public void ClearPath()
    {
        pathViz.positionCount = 0;
    }
    #endregion

    #region Update
    private void Update()
    {
        if (!isInit) return;

        delta = Time.deltaTime;

        if (turns[turnIndex].Execute(this))
        {
            turnIndex++;

            if (turnIndex > turns.Length - 1)
            {
                turnIndex = 0;
            }
        }
    }
    #endregion
}

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

    public void PathfinderCall(Node targetNode)
    {
        if (!isPathfinding)
        {
            isPathfinding = true;

            Node start = turns[0].player.characters[0].currentNode;
            if (start != null && targetNode != null)
            {
                PathfinderMaster
                    .singleton
                    .RequestPathfind(turns[0].player.characters[0],
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

        pathViz.positionCount = path.Count;
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            positions.Add(path[i].worldPosition + Vector3.up * .1f);
        }

        pathViz.SetPositions(positions.ToArray());
    }

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
}

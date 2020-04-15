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
    public LineRenderer pathVizNotReachable;

    public VariablesHolder gameVariables;

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

        List<Node> pathActual = new List<Node>();

        List<Vector3> reachablePositions = new List<Vector3>();
        List<Vector3> unreachalePositions = new List<Vector3>();
        reachablePositions.Add(character.currentNode.worldPosition);

        if (character.actionPoints > 0)
        {
            reachablePositions.Add(character.currentNode.worldPosition);
        }

        if (path.Count > character.actionPoints)
        {
            if (character.actionPoints == 0)
            {
                unreachalePositions.Add(character.currentNode.worldPosition);
            }
            else
            {
                unreachalePositions.Add(path[character.actionPoints - 1].worldPosition);
            }
        }

        for (int i = 0; i < path.Count; i++)
        {
            if (i <= character.actionPoints - 1)
            {
                pathActual.Add(path[i]);
                reachablePositions.Add(path[i].worldPosition);
            }
            else
            {
                unreachalePositions.Add(path[i].worldPosition);
            }
        }

        pathViz.positionCount = reachablePositions.Count;
        pathViz.SetPositions(reachablePositions.ToArray());
        pathVizNotReachable.positionCount = unreachalePositions.Count;
        pathVizNotReachable.SetPositions(unreachalePositions.ToArray());

        character.LoadPath(pathActual);
    }

    public void ClearPath(StateManager stateManager)
    {
        pathViz.positionCount = 0;
        pathVizNotReachable.positionCount = 0;

        if (stateManager.currentCharacter != null)
        {
            stateManager.currentCharacter.currentPath = null;
        }
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

    public void EndTurn()
    {
        turns[turnIndex].EndCurrentPhase(this);
    }
    #endregion
}

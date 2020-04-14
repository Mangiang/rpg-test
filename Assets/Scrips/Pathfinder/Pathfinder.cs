using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    GridManager gridManager;

    GridCharacter character;

    Node startNode;

    Node endNode; // because coding A* algo

    public volatile float timer;

    public volatile bool jobDone = false;

    public delegate void
        PathfindingComplete(List<Node> nodes, GridCharacter character);

    PathfindingComplete completeCallback;

    List<Node> targetPath;

    public Pathfinder(
        GridCharacter character,
        Node startNode,
        Node targetNode,
        PathfindingComplete callback,
        GridManager gridManager
    )
    {
        this.character = character;
        this.startNode = startNode;
        this.endNode = targetNode;
        this.completeCallback = callback;
        this.gridManager = gridManager;
    }

    public void FindPath()
    {
        targetPath = FincPathActual();
        jobDone = true;
    }

    public void NotifyComplete()
    {
        if (completeCallback != null) completeCallback(targetPath, character);
    }

    List<Node>
    FincPathActual() // Note : compute only 2 dimensional and aim for ladders to climb up/down
    {
        List<Node> foundPath = new List<Node>();
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 0; i < openSet.Count; i++)
            {
                if (
                    openSet[i].fCost < currentNode.fCost ||
                    (
                    openSet[i].fCost == currentNode.fCost &&
                    openSet[i].hCost < currentNode.hCost
                    )
                )
                {
                    if (!currentNode.Equals(openSet[i]))
                    {
                        currentNode = openSet[i];
                    }
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (currentNode.Equals(endNode))
            {
                foundPath = RetracePath(startNode, currentNode);
                break;
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!closedSet.Contains(neighbour))
                {
                    float newMovementCostToNeighbour =
                        currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (
                        newMovementCostToNeighbour < neighbour.gCost ||
                        !openSet.Contains(neighbour)
                    )
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, endNode);
                        neighbour.parentNode = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        return foundPath;
    }

    int GetDistance(Node posA, Node posB)
    {
        int distX = Mathf.Abs(posA.x - posB.x);
        int distZ = Mathf.Abs(posA.z - posB.z);

        if (distX > distZ)
        {
            return 14 * distZ + 10 * (distX - distZ);
        }

        return 14 * distX + 10 * (distZ - distX);
    }

    List<Node> GetNeighbours(Node node)
    {
        List<Node> retList = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0)
                {
                    continue;
                }

                int _x = x + node.x;
                int _y = node.y;
                int _z = z + node.z;

                Node currNode = GetNode(_x, _y, _z);

                if (currNode != null && !node.CanGoTo(currNode)) continue;

                Node newNode = GetNeighbour(currNode);

                if (newNode == null) continue;

                retList.Add(newNode);
            }
        }

        return retList;
    }

    Node GetNode(int x, int y, int z)
    {
        return gridManager.GetNode(x, y, z);
    }

    Node GetNeighbour(Node node)
    {
        Node retVal = null;

        if (node != null && node.isWalkable)
        {
            retVal = node;
        }

        return retVal;
    }

    List<Node> RetracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();

        Node currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        path.Reverse();
        return path;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;

    public int y;

    public int z;

    public bool isWalkable;

    public Vector3 worldPosition;

    public GridObject obstacle;

    // Left, Right, Front, Back, Up
    // Bottom is the floor
    public GridObject wallForward;

    public GridObject wallBack;

    public GridObject wallRight;

    public GridObject wallLeft;

    public GridObject wallTop;

    public GridCharacter character;


#region Used by A* algo
    public float hCost;

    public float gCost;

    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
#endregion


    public Node parentNode;

    public bool CanGoTo(Node targetNode)
    {
        if (targetNode.x == x + 1 && wallRight)
        {
            return false;
        }

        if (targetNode.x == x - 1 && wallLeft)
        {
            return false;
        }

        if (targetNode.z == z + 1 && wallForward)
        {
            return false;
        }

        if (targetNode.z == z - 1 && wallBack)
        {
            return false;
        }

        return true;
    }
}

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

    public GridObject[] walls = new GridObject[(int)NodeDirectionEnum.LENGTH];
    public Node[] canGoTo = new Node[(int)NodeDirectionEnum.LENGTH];
    public Node[] isBlocked = new Node[(int)NodeDirectionEnum.LENGTH];

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
        for (int idx = 0; idx < canGoTo.Length; idx++)
        {
            if (canGoTo[idx] == targetNode)
            {
                return true;
            }
        }

        return false;
    }
}

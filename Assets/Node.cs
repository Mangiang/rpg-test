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
}

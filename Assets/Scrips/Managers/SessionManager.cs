using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    int turnIndex;

    public Turn[] turns;

    public GridManager gridManager;

    bool isInit;

    private void Start()
    {
        gridManager.Init();
        PlaceUnits();
        isInit = true;
    }

    void PlaceUnits()
    {
        GridCharacter[] units = GameObject.FindObjectsOfType<GridCharacter>();
        foreach (GridCharacter unit in units)
        {
            Node n = gridManager.GetNode(unit.transform.position);
            if (n != null)
            {
                unit.transform.position = n.worldPosition;
                n.character = unit;
                unit.currentNode = n;
            }
        }
    }

    private void Update()
    {
        if (!isInit) return;

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

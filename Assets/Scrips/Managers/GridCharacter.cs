using System.Collections.Generic;
using UnityEngine;

public class GridCharacter : MonoBehaviour, ISelectable, IDeselectable, IHighlight, IDehighlight, IDetectableByMouse
{
    public PlayerHolder owner;
    public GameObject highlighter;

    public float moveSpeed;

    public Character character;

    public int actionPoints;

    [HideInInspector] public Node currentNode;

    [HideInInspector] bool isSelected;

    [HideInInspector] public List<Node> currentPath;

    public void LoadPath(List<Node> path)
    {
        currentPath = path;
    }

    public void OnInit()
    {
        owner.RegisterCharacter(this);
    }

    public void OnStartTurn()
    {
        actionPoints = character.StartingAP;
    }

    public void OnSelect(PlayerHolder playerHolder)
    {
        isSelected = true;
        highlighter.SetActive(true);
        playerHolder.stateManager.currentCharacter = this;
    }
    public void OnDeselect(PlayerHolder playerHolder)
    {
        isSelected = false;
        highlighter.SetActive(false);
    }

    public void OnHighlight(PlayerHolder playerHolder)
    {
        highlighter.SetActive(true);
    }
    public void OnDehighlight(PlayerHolder playerHolder)
    {
        if (!isSelected)
        {
            highlighter.SetActive(false);
        }
    }

    public Node OnDetec()
    {
        return currentNode;
    }
}

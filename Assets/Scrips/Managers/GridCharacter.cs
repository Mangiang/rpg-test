using UnityEngine;

public class GridCharacter : MonoBehaviour, ISelectable, IDeselectable, IHighlight, IDehighlight, IDetectableByMouse
{
    public PlayerHolder owner;

    public Node currentNode;

    public GameObject highlighter;

    bool isSelected;

    public void OnInit()
    {
        owner.RegisterCharacter(this);
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

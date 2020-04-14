using UnityEngine;

public class GridCharacter : MonoBehaviour
{
    public PlayerHolder owner;

    public Node currentNode;

    public void OnInit()
    {
        owner.RegisterCharacter(this);
    }
}

using UnityEngine;

public class DetectMousePosition : StateActions
{
    public override void Execute(
        StateManager stateManager,
        SessionManager sm,
        Turn turn
    )
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Node node = sm.gridManager.GetNode(hit.point);
            stateManager.currentNode = node;
            if (node != null)
            {
                Debug.Log("Node Found");
            }
        }
    }
}

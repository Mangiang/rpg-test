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
            stateManager.currentNode = sm.gridManager.GetNode(hit.point);
            if (stateManager.currentNode != null)
            {
                if (stateManager.currentNode != stateManager.prevNode)
                {
                    stateManager.prevNode = stateManager.currentNode;
                    sm.PathfinderCall(stateManager.currentNode);
                }
            }
        }
    }
}

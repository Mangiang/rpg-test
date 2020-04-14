using UnityEngine;

public class HandleMouseInteractions : StateActions
{
    GridCharacter prevCharacter;

    public override void Execute(
        StateManager stateManager,
        SessionManager sm,
        Turn turn
    )
    {
        bool mouseClick = Input.GetMouseButtonDown(0);
        if (prevCharacter != null)
        {
            prevCharacter.OnDehighlight(stateManager.playerHolder);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Debug
            .DrawLine(ray.origin, ray.origin + ray.direction * 1000, Color.red);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            stateManager.currentNode = sm.gridManager.GetNode(hit.point);
            IDetectableByMouse detectable = hit.transform.GetComponent<IDetectableByMouse>();
            if (detectable != null)
            {
                stateManager.currentNode = detectable.OnDetec();
            }

            Node currentNode = stateManager.currentNode;
            if (currentNode != null)
            {
                if (currentNode.character != null)
                {
                    if (currentNode.character.owner == stateManager.playerHolder)
                    {
                        currentNode.character.OnHighlight(stateManager.playerHolder);
                        prevCharacter = currentNode.character;
                    }
                    else
                    {

                    }
                }
                if (stateManager.currentCharacter != null && currentNode.character == null)
                {
                    if (mouseClick)
                    {

                        if (stateManager.currentCharacter)
                        {
                            if (stateManager.currentCharacter.currentPath != null || stateManager.currentCharacter.currentPath.Count > 0)
                            {
                                stateManager.SetState("moveOnPath");
                            }
                        }
                    }
                    else
                    {
                        PathDetection(stateManager, sm, currentNode);
                    }
                }
                else
                {
                    if (mouseClick)
                    {

                        if (currentNode.character.owner == stateManager.playerHolder)
                        {
                            currentNode.character.OnSelect(stateManager.playerHolder);
                            stateManager.prevNode = null;
                            sm.ClearPath(stateManager);
                        }
                    }
                }
            }
        }
    }

    void PathDetection(StateManager stateManager, SessionManager sessionManager, Node node)
    {
        stateManager.currentNode = node;
        if (stateManager.currentNode != null)
        {
            if (stateManager.currentNode != stateManager.prevNode)
            {
                stateManager.prevNode = stateManager.currentNode;
                sessionManager.PathfinderCall(stateManager.currentCharacter, stateManager.currentNode);
            }
        }
    }
}

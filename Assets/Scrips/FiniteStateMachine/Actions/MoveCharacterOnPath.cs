using UnityEngine;

public class MoveCharacterOnPath : StateActions
{
    bool isInit;

    float time;
    float rotTime;
    float speed;
    float rotSpeed;

    Node startNode;
    Node targetNode;
    int index;
    Quaternion targetRot;
    Quaternion startRot;

    public override void Execute(StateManager stateManager, SessionManager sm, Turn turn)
    {
        GridCharacter gridCharacter = stateManager.currentCharacter;
        if (!isInit)
        {
            if (gridCharacter == null || index > gridCharacter.currentPath.Count - 1)
            {
                stateManager.SetStartingState();
                return;
            }

            isInit = true;
            startNode = gridCharacter.currentNode;
            targetNode = gridCharacter.currentPath[index];
            float timer = time - 1;
            timer = Mathf.Clamp01(timer);
            time = timer;

            float distance = Vector3.Distance(startNode.worldPosition, targetNode.worldPosition);
            speed = gridCharacter.moveSpeed / distance;

            Vector3 direction = targetNode.worldPosition - startNode.worldPosition;
            targetRot = Quaternion.LookRotation(direction);
            startRot = gridCharacter.transform.rotation;
        }

        time += Time.deltaTime * speed;
        rotTime += Time.deltaTime * rotSpeed;

        if (rotTime > 1)
        {
            rotTime = 1;
        }
        Quaternion tr = Quaternion.Lerp(startRot, targetRot, rotTime);
        gridCharacter.transform.rotation = tr;

        if (time > 1)
        {
            isInit = false;
            gridCharacter.currentNode.character = null;
            gridCharacter.currentNode = targetNode;
            targetNode.character = gridCharacter;

            index++;

            if (index > gridCharacter.currentPath.Count - 1)
            {
                time = 1;

                index = 0;

                stateManager.SetStartingState();
            }
        }

        Vector3 tp = Vector3.Lerp(startNode.worldPosition, targetNode.worldPosition, time);
        gridCharacter.transform.position = tp;

    }
}
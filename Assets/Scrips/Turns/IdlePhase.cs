using UnityEngine;

[CreateAssetMenu(menuName = "Phases/Idle Phase")]
public class IdlePhase : Phase
{
    IdlePhase()
    {
        phaseName = "Idle Phase";
    }

    public override bool IsComplete(SessionManager sm)
    {
        return false;
    }

    public override void OnStartPhase(SessionManager sm)
    {
        if (isInit)
        {
            return;
        }
        isInit = true;
        Debug.Log($"Stating {phaseName} phase");
    }
}

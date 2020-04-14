using UnityEngine;

[CreateAssetMenu(fileName = "Turn", menuName = "Game/Turn", order = 0)]
public class Turn : ScriptableObject
{
    public PlayerHolder player;

    public int phaseIndex;

    public Phase[] phases;

    public bool Execute(SessionManager sm)
    {
        bool result = false;

        phases[phaseIndex].OnStartPhase(sm);

        if (phases[phaseIndex].IsComplete(sm))
        {
            phases[phaseIndex].OnEndPhase(sm);
            phaseIndex++;
            if (phaseIndex > phases.Length - 1)
            {
                phaseIndex = 0;
                result = true;
            }
        }

        return result;
    }

    public void EndCurrentPhase(SessionManager sm)
    {
        phases[phaseIndex].exit = true;
    }
}

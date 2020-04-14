using UnityEngine;

[CreateAssetMenu(fileName = "Turn", menuName = "Game/Turn", order = 0)]
public class Turn : ScriptableObject
{
    public PlayerHolder player;

    [System.NonSerialized]
    public int phaseIndex = 0;

    public Phase[] phases;

    public bool Execute(SessionManager sm)
    {
        bool result = false;

        phases[phaseIndex].OnStartPhase(sm, this);

        if (phases[phaseIndex].IsComplete(sm, this))
        {
            phases[phaseIndex].OnEndPhase(sm, this);
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

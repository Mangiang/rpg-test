using UnityEngine;

[CreateAssetMenu(menuName = "Phases/States Tick")]
public class StatesTickPhase : Phase
{
    public override bool IsComplete(SessionManager sm, Turn turn)
    {
        if (exit) return true;
        turn.player.stateManager.Tick(sm, turn);
        return false;
    }

    public override void OnStartPhase(SessionManager sm, Turn turn)
    {
    }
}

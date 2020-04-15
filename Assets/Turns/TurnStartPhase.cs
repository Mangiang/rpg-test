using UnityEngine;

[CreateAssetMenu(menuName = "Phases/Turn Start Phase")]
public class TurnStartPhase : Phase
{
    public override bool IsComplete(SessionManager sm, Turn turn)
    {
        return true;
    }

    public override void OnStartPhase(SessionManager sm, Turn turn)
    {
        foreach (GridCharacter character in turn.player.characters)
        {
            character.OnStartTurn();

            if (turn.player.stateManager.currentCharacter == character)
            {
                sm.gameVariables.actionPointsInt.value = character.actionPoints;
            }
        }
    }
}
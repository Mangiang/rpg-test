using System.Collections.Generic;

public class State
{
    public List<StateActions> actions = new List<StateActions>();

    public void Tick(
        StateManager stateManager,
        SessionManager sessionManager,
        Turn turn
    )
    {
        if (stateManager.forceExit) return;

        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Execute(stateManager, sessionManager, turn);
        }
    }
}

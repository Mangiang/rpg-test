using UnityEngine;

public class InputManager : StateActions
{
    VariablesHolder variablesHolder;

    public InputManager(VariablesHolder holder)
    {
        variablesHolder = holder;
    }

    public override void Execute(
        StateManager stateManager,
        SessionManager sm,
        Turn turn
    )
    {
        variablesHolder.horizontal.value = Input.GetAxis("Horizontal");
        variablesHolder.vertical.value = Input.GetAxis("Vertical");
    }
}

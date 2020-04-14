using UnityEngine;

public class PlayerStateManager : StateManager
{
    public override void Init()
    {
        VariablesHolder variablesHolder =
            Resources.Load("GameVariables") as VariablesHolder;
        State interactions = new State();
        interactions.actions.Add(new InputManager(variablesHolder));
        interactions.actions.Add(new HandleMouseInteractions());
        interactions.actions.Add(new ModeCameraTransform(variablesHolder));

        State wait = new State();

        currentState = interactions;

        states.Add("interactions", interactions);
        states.Add("wait", wait);
    }
}

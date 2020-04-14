public class PlayerStateManager : StateManager
{
    public override void Init()
    {
        State interactions = new State();
        interactions.actions.Add(new DetectMousePosition());

        State wait = new State();

        currentState = interactions;

        states.Add("interactions", interactions);
        states.Add("wait", wait);
    }
}

public abstract class StateActions
{
    public abstract void Execute(
        StateManager stateManager,
        SessionManager sm,
        Turn turn
    );
}

using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    public State currentState;

    public bool forceExit; // ForceExit state

    public Node currentNode;

    public Node prevNode;

    protected Dictionary<string, State> states = new Dictionary<string, State>();

    public float delta; // Delta time

    public PlayerHolder playerHolder;
    public GridCharacter currentCharacter
    {
        get
        {
            return _currentCharacter;
        }
        set
        {
            if (_currentCharacter != null)
            {
                _currentCharacter.OnDeselect(playerHolder);
            }
            _currentCharacter = value;
        }
    }
    GridCharacter _currentCharacter;

    private void Start()
    {
        Init();
    }

    public abstract void Init();

    public void Tick(SessionManager sm, Turn turn)
    {
        delta = sm.delta;
        if (currentState != null)
        {
            currentState.Tick(this, sm, turn);
        }

        forceExit = false;
    }

    public void SetState(string id)
    {
        State targetState = GetState(id);

        if (targetState == null)
        {
            Debug.LogError($"State with id: {id} cannot be found");
        }

        currentState = targetState;
    }

    State GetState(string id)
    {
        State result = null;

        states.TryGetValue(id, out result);

        return result;
    }
}

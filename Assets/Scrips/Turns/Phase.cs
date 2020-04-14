using UnityEngine;

[CreateAssetMenu(fileName = "Phase", menuName = "Game/Phase", order = 1)]
public abstract class Phase : ScriptableObject
{
    public string phaseName;

    public bool exit;

    public abstract bool IsComplete(SessionManager sm, Turn turn);

    [System.NonSerialized]
    protected bool isInit;

    public abstract void OnStartPhase(SessionManager sm, Turn turn);

    public virtual void OnEndPhase(SessionManager sm, Turn turn)
    {
        isInit = false;
        exit = false;
        Debug.Log($"Ending {phaseName} phase");
    }
}

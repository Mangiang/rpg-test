using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Holder")]
public class PlayerHolder : ScriptableObject
{
    [System.NonSerialized]
    public StateManager stateManager;

    [System.NonSerialized]
    GameObject stateManagerObject;

    [SerializeField]
    GameObject stateManagerPrefab;

    public List<GridCharacter> characters = new List<GridCharacter>();

    public void Init()
    {
        stateManagerObject = Instantiate(stateManagerPrefab) as GameObject;
        stateManager = stateManagerObject.GetComponent<StateManager>();
    }

    public void RegisterCharacter(GridCharacter character)
    {
        if (characters.Contains(character) == false)
        {
            characters.Add (character);
        }
    }

    public void UnRegisterCharacter(GridCharacter character)
    {
        if (characters.Contains(character))
        {
            characters.Remove (character);
        }
    }
}

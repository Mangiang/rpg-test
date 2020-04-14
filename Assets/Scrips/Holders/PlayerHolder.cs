using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Holder")]
public class PlayerHolder : ScriptableObject
{
    public List<GridCharacter> characters = new List<GridCharacter>();

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

using UnityEngine;

[CreateAssetMenu(menuName = "Characters/character")]
public class Character : ScriptableObject
{
    public string name;
    public int agility = 10;
    public int StartingAP
    {
        get
        {
            return agility;
        }
    }
}
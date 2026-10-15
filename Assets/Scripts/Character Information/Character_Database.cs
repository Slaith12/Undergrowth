using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Database", menuName = "Scriptable Objects/Character_Database")]
public class Character_Database : ScriptableObject
{
    [SerializeField] private List<Character_Data> characters;

    public Character_Data GetCharacter(string characterId)
    {
        return characters.Find(c => c.CharacterId.Equals(characterId, System.StringComparison.OrdinalIgnoreCase));
    }
}

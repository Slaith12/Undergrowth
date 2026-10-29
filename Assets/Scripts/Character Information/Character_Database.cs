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

    public List<Character_Data> GetAllCharacters()
    {
        List<Character_Data> readyCharacters = new List<Character_Data>();
        foreach (Character_Data character in characters)
        {
            // TODO: In here, we can determine whether a character 
            // is 'ready' to visit the shop or not. This is where we can adjust 
            // the weights of who is more likely to show up 
            readyCharacters.Add(character);
        }
        return readyCharacters;
    }
}

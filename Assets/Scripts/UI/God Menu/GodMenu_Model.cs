using Ink.Parsed;
using UnityEngine;
using System.Collections.Generic;
public class GodMenu_Model
{
    public int CharacterIndex { get; private set; }

    public List<string> CharacterOptions { get; private set; }

    public GodMenu_Model(int defaultCharacterIndex, string defaultCharacter, Character_Database character_Database)
    {
        CharacterIndex = defaultCharacterIndex;
        CharacterOptions = new List<string>();

        for (int i = 0; i < character_Database.characters.Count; i++)
        {
            CharacterOptions.Add(character_Database.characters[i].DisplayName);
        }
    }

    public void SetActiveCharacter(int index) => CharacterIndex = index;
}

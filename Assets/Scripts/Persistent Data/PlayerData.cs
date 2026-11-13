using Ink.Parsed;
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CharacterSaveData
{
    public string characterId;
    public float weight; 

    public CharacterSaveData(string characterId, float weight)
    {
        this.characterId = characterId;
        this.weight = weight;
    }
}

[Serializable]
public class PlayerData
{
    public float currentMoney;
    public int currentDay;
    public List<CharacterSaveData> characterWeights = new List<CharacterSaveData>();

    public void SetCharacterWeight(string id, float weight)
    {
        CharacterSaveData existing = characterWeights.Find(c => c.characterId == id); 
        if (existing != null)
        {
           existing.weight = weight;
        }
        else
        {
            characterWeights.Add(new CharacterSaveData(id, weight));
        }
    }

    public float GetCharacterWeight(string id, float fallbackWeight = 0f)
    {
        CharacterSaveData existing = characterWeights.Find(c => c.characterId == id);
        return existing != null ? existing.weight : fallbackWeight;
    }

}

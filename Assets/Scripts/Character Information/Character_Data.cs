using Ink.Parsed;
using UnityEngine;
using System.Collections.Generic;
using System;
using Ink.UnityIntegration;

[CreateAssetMenu(fileName = "Character_Data", menuName = "Scriptable Objects/Character_Data")]
public class Characater_Data : ScriptableObject
{
    [SerializeField] private string characterId;
    [SerializeField] private string displayName;
    [SerializeField] private List<SpriteEntry> sprites;
    [SerializeField] private InkFile scriptJson;

    public string CharacterId => characterId; 
    public string DisplayName => displayName;
    public InkFile ScriptJson => scriptJson;

    public Sprite GetSprite(string emotionKey)
    {
        var entry = sprites.Find(p => p.emotion.Equals(emotionKey, StringComparison.OrdinalIgnoreCase));
        return entry.sprite;
    }

    [Serializable]
    private struct SpriteEntry
    {
        public string emotion;
        public Sprite sprite;
    }



}

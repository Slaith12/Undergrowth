using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath => Path.Combine(Application.persistentDataPath, "player_data.json");
    public static bool HasSaveFile() => File.Exists(savePath);

    public static void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("File saved at: " + savePath);
    }

    public static PlayerData LoadPlayerData()
    {
        if (!HasSaveFile()) return null;

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}

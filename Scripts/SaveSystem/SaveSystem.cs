using System.IO;
using Player;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveSystem
{
    private static string SavePath = Path.Combine(Application.persistentDataPath, "player.json");

    public static void SavePlayer(PlayerData data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(SavePath, json);
    }

    public static PlayerData LoadPlayer()
    {
        if (!File.Exists(SavePath))
        {
            return new PlayerData();
        }
        string json = File.ReadAllText(SavePath);
        return JsonConvert.DeserializeObject<PlayerData>(json);
    }
}

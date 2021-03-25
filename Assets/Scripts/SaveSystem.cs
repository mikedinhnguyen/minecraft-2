using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SavePlayer(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/player.save";

        FileStream stream = File.Create(path);

        PlayerData data = new PlayerData(score);

        formatter.Serialize(stream, data);
        Debug.Log("saved data");
        stream.Close();
    }

    public static int LoadPlayer()
    {
        string path = Application.persistentDataPath + "/saves/player.save";

        if (!File.Exists(path))
        {
            Debug.Log("no save file");
            return 0;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(path, FileMode.Open);

        try
        {
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data.highScore;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            stream.Close();
            return 0;
        }
    }
}

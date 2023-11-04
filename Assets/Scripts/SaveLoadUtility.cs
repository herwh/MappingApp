using UnityEngine;
using System.IO;

public static class SaveLoadUtility
{
    public static void SaveGame(GameData gameData)
    {
        var json = JsonUtility.ToJson(gameData, true);

        var dirPath = Application.persistentDataPath + "/GameData";
        var fullPath = dirPath + "/gamedata.json";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        if (!File.Exists(fullPath))
        {
            using (File.Create(fullPath))
            {

            }
        }

        File.WriteAllText(fullPath, json);
    }

    public static GameData LoadGame()
    {
        var dirPath = Application.persistentDataPath + "/GameData";
        var fullPath = dirPath + "/gamedata.json";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        if (!File.Exists(fullPath))
        {
            return new GameData();
        }

        var json = File.ReadAllText(fullPath);
        var gameData = JsonUtility.FromJson<GameData>(json);

        return gameData;
    }

    public static Texture LoadImage(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        var texture = new Texture2D(3, 3);
        texture.LoadImage(File.ReadAllBytes(path));

        return texture;
    }

    public static string SaveAsImage(byte[] imageData)
    {
        var dirPath = Application.persistentDataPath + "/SaveImages/";
        var fullPath = $"{dirPath}{imageData.GetHashCode()}.png";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllBytes(fullPath, imageData);

        return fullPath;
    }
    
    public static void DeleteImage(string imagePath)
    {
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}
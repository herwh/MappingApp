using UnityEngine;
using System.IO;
using DefaultNamespace;

public class SaveLoadManager
{
    private string _filePath;

    public void SaveGame(GameData gameData)
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

    public GameData LoadGame()
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

    private void Start()
    {
        _filePath = Application.persistentDataPath + "/save.pinsave";
    }
}
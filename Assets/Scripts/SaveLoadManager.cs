using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private string _filePath;
    
    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {
        
    }

    private void Start()
    {
        _filePath = Application.persistentDataPath + "/save.pinsave";
    }
}

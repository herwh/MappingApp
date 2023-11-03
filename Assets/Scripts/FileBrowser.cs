using System.Collections;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileBrowser : MonoBehaviour
{
    [SerializeField]private RawImage _image;
    public void InputImageButtonOnClick()//сделать ч/з подписку и отписку
    {
        OpenFilePanel();
    }

    private void Start()
    {
        LoadImage();//отрефакторить
    }

    private void OpenFilePanel()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "png", false);

        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutineOpen(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
    
    private IEnumerator OutputRoutineOpen(string url)
    {
       UnityWebRequest www = UnityWebRequest.Get(url);

       yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW Error: " + www.error);
        }
        else
        {
            SaveImage(www);
            UnityEditor.AssetDatabase.Refresh();
        }
    }

    private void SaveImage(UnityWebRequest www)
    {
        var bytes = www.downloadHandler.data;

        var texture = new Texture2D(3, 3);
        texture.LoadImage(bytes);
        _image.texture = texture;

        var dirPath = Application.dataPath + "/SaveImages/";
        var fullPath = dirPath + "Image" + ".png";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllBytes(fullPath, bytes);
    }
    
    private void LoadImage()
    {
        var dirPath = Application.dataPath + "/SaveImages/";
        var fullPath = dirPath + "Image" + ".png";

        if (!File.Exists(fullPath))
        {
            return;
        }

        var texture = new Texture2D(3, 3);
        texture.LoadImage(File.ReadAllBytes(fullPath));

        _image.texture = texture;
    }
}
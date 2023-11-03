using System.Collections;
using System.IO;
using SFB;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileBrowser
{
    private readonly RawImage _previewImage;
    
    private byte[] _selectedImageData;

    public FileBrowser(RawImage previewImage)
    {
        _previewImage = previewImage;
    }
    
    public void ImageButtonClicked() //сделать ч/з подписку и отписку
    {
        OpenFilePanel();
    }

    public string SaveSelectedImage()
    {
        var dirPath = Application.persistentDataPath + "/SaveImages/";
        var fullPath = $"{dirPath}{_selectedImageData.GetHashCode()}.png";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllBytes(fullPath, _selectedImageData);

        return fullPath;
    }

    private void OpenFilePanel()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "png", false);

        if (paths.Length > 0)
        {
            _previewImage.StartCoroutine(OutputRoutineOpen(new System.Uri(paths[0]).AbsoluteUri));
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
            UpdateImage(www.downloadHandler.data);
        }
    }

    private void UpdateImage(byte[] imageData)
    {
        _selectedImageData = imageData;
        var texture = new Texture2D(3, 3); //refactor

        texture.LoadImage(_selectedImageData);
        _previewImage.texture = texture;
    }
}
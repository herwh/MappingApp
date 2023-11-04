using System.Collections;
using SFB;
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

    public void ImageButtonClicked()
    {
        OpenFilePanel();
    }

    public byte[] GetSelectedImageData()
    {
        return _selectedImageData;
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
        var texture = new Texture2D(3, 3);

        texture.LoadImage(_selectedImageData);
        _previewImage.texture = texture;
    }
}
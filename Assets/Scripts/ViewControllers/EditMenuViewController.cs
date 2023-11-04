using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditMenuViewController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _title;
    [SerializeField] private TMP_InputField _description;
    [SerializeField] private Button _setImageButton;
    [SerializeField] private Button _submitButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private RawImage _image;
    [SerializeField] private Texture _defaultTexture;
    
    public event Action SubmitClicked=delegate {  };
    public event Action CancelClicked=delegate {  };

    private FileBrowser _fileBrowser;
    
    public string GetTitle()
    {
        return _title.text;
    }

    public string GetDescription()
    {
        return _description.text;
    }

    public string GetImagePath()
    {
        return SaveLoadUtility.SaveAsImage(_fileBrowser.GetSelectedImageData());
    }
    
    private void Start()
    {
        _fileBrowser = new FileBrowser(_image);
        
        _submitButton.onClick.AddListener(SubmitButtonClicked);
        _setImageButton.onClick.AddListener(SetImageButtonClicked);
        _cancelButton.onClick.AddListener(CancelButtonClicked);
    }

    private void OnEnable()
    {
        _image.texture = _defaultTexture;
    }

    private void SetImageButtonClicked()
    {
        _fileBrowser.ImageButtonClicked();
    }

    private void SubmitButtonClicked()
    {
        SubmitClicked();
        gameObject.SetActive(false);
    }

    private void CancelButtonClicked()
    {
        CancelClicked();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _submitButton.onClick.RemoveListener(SubmitButtonClicked);
        _setImageButton.onClick.RemoveListener(SetImageButtonClicked);
        _cancelButton.onClick.RemoveListener(CancelButtonClicked);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditMenuViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _setImageButton;
    [SerializeField] private Button _submitButton;
    [SerializeField] private Button _cancelButton;

    public event Action SubmitClicked=delegate {  };
    public event Action CancelClicked=delegate {  };
    public Pin Pin;

    public string GetTitle()
    {
        return _title.text;
    }

    public string GetDescription()
    {
        return _description.text;
    }
    
    private void Start()
    {
        _submitButton.onClick.AddListener(SubmitButtonClicked);
        _cancelButton.onClick.AddListener(CancelButtonClicked);
    }
    
    private void SubmitButtonClicked()
    {
        SubmitClicked();
    }

    private void CancelButtonClicked()
    {
        CancelClicked();
    }

    private void OnDestroy()
    {
        _submitButton.onClick.RemoveListener(SubmitButtonClicked);
        _cancelButton.onClick.RemoveListener(CancelButtonClicked);
    }
}

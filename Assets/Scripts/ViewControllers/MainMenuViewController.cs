using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private RawImage _image;
    [SerializeField] private Button _closeButton;
    
    public void SetContent(string title, string description, Texture image)
    {
        _title.text = title;
        _description.text = description;
        _image.texture = image;
    }

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseButtonClicked);
    }

    private void CloseButtonClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(CloseButtonClicked);
    }
}

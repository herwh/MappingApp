using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class MainMenuViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _image;
    
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MenuViewController : MonoBehaviour
{
    [SerializeField] private Button _addNewPinButton;
    [SerializeField] private Button _saveAllPinsButton;
    [SerializeField] private Pin _pinPrefab;

    private void Start()
    {
        _addNewPinButton.onClick.AddListener(AddNewPin);
        _saveAllPinsButton.onClick.AddListener(SaveAllPins);
    }

    private void AddNewPin()
    {
        Instantiate(_pinPrefab, transform);
    }

    private void SaveAllPins()
    {
        
    }

    private void OnDestroy()
    {
        _addNewPinButton.onClick.RemoveListener(AddNewPin);
        _saveAllPinsButton.onClick.RemoveListener(SaveAllPins);
    }
}

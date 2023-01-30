using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateDeck_Marker : MonoBehaviour
{
    private InputField _inputField;    
    public string _currentDeckName;
    /// <summary>
    /// префаб шаблона созданной колоды
    /// </summary>
    public GameObject _prefabTemplateDeck;


    private void Awake()
    {
        _inputField = FindObjectOfType<InputTextMarker>().GetComponent<InputField>();
        _inputField.onEndEdit.AddListener(ToText);
    }
    public void ToText(string inputText)
    {        
        _currentDeckName = inputText;
        Debug.Log(_currentDeckName);
    }

}

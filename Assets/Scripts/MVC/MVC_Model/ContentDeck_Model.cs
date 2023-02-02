using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ContentDeck_Model : MonoBehaviour
    {
        public InputField _inputField;
        /// <summary>
        /// ������� �������� ������
        /// </summary>
        public string _currentDeckName;        
        /// <summary>
        /// ������ ������� ������������� ������� ��������� ��������� �����
        /// </summary>
        public GameObject _prefabChioseCardSettings;
        /// <summary>
        /// ������� ������ ID ��������� ���� � ������
        /// </summary>
        public List<int> _contentDeck;

        private void Awake()
        {
            _inputField = FindObjectOfType<InputTextMarker>().GetComponent<InputField>();
            _inputField.onEndEdit.AddListener(ToText);
        }
        public void ToText(string inputText)
        {
            _currentDeckName = inputText;
            Debug.Log($"������ ������ {_currentDeckName}");
        }

    }
}
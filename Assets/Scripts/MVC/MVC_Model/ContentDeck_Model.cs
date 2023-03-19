using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ContentDeck_Model : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        /// <summary>
        /// ������� �������� ������
        /// </summary>
        [SerializeField] private string _currentDeckName;
        /// <summary>
        /// ��� ��������� ���� , ������� ����� ��������� � ������
        /// </summary>
        [SerializeField] private CardClasses _classHeroInDeck;
        /// <summary>
        /// ������ ������� ������������� ������� ��������� ��������� �����
        /// </summary>
        [SerializeField] private GameObject _prefabChioseCardSettings;
        /// <summary>
        /// ������� ������ ID ��������� ���� � ������
        /// </summary>
        [SerializeField] private List<int> _contentDeck;

        public List<int> ContentDeck { get => _contentDeck; set => _contentDeck = value; }
        public InputField InputField { get => _inputField; }
        public string CurrentDeckName { get => _currentDeckName; set => _currentDeckName = value; }
        public CardClasses ClassHeroInDeck { get => _classHeroInDeck; set => _classHeroInDeck = value; }
        public GameObject PrefabChioseCardSettings { get => _prefabChioseCardSettings; }

        private void Awake()
        {
            _classHeroInDeck = CardClasses.Universal;
            _inputField = FindObjectOfType<InputTextMarker>().GetComponent<InputField>();
            _inputField.onEndEdit.AddListener(ToText);
        }
        public void ToText(string inputText)
        {
            _currentDeckName = inputText;            
        }
    }
}
using System.Xml;
using UnityEngine;
//using System.Xml.Linq;

namespace Hearthstone
{
    public class Storage : MonoBehaviour
    {
        //private readonly string _path = "Assets/Resources/StorageConfig.xml";
        //private int _index = 0;
        //XmlDocument _storage;
        //XmlElement xRoot;  
        private int[] _cardIdColections = new int[] { 1, 15, 20, 205, 45, 15, 24 };

        private string _cardName = "DeckWarior";
        XmlDocument _xmlDoc;
        XmlElement _elemDeck;
        XmlElement _elemAreyIDCard;
        //private int _id = 0;

        private string _path;




        private void Start()
        {
            _path = Application.dataPath + "/SaveData.xml";
            _xmlDoc = new XmlDocument();
            XmlNode rootNode = _xmlDoc.CreateElement("RootElement");
            _xmlDoc.AppendChild(rootNode);
            _elemDeck = _xmlDoc.CreateElement(_cardName);
            _elemDeck.SetAttribute(name, "Первая колода воина");
            rootNode.AppendChild(_elemDeck);

            SetDeck();

        }

        private void SetDeck()
        {
            _elemAreyIDCard = _xmlDoc.CreateElement("AreyIDCard");
            _elemAreyIDCard.SetAttribute("Колличество", "30");
            _elemDeck.AppendChild(_elemAreyIDCard);
            _xmlDoc.Save(_path);
        }

        private void SetCard()
        {
            for (int i = 0; i < _cardIdColections.Length; i++)
            {
                XmlElement elemIdCard = _xmlDoc.CreateElement($"id{_cardIdColections[i]}");
                _elemAreyIDCard.AppendChild(elemIdCard);
                Debug.Log($"Добавлена новая карта в колоду c ID{_cardIdColections[i]}");
            }
            _xmlDoc.Save(_path);

        }

        private void DeleteCard()
        {
            //xDoc.Save(_path);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetCard();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                DeleteCard();
            }
        }
    }
}
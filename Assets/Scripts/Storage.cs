using System.Collections.Generic;
using System.Xml;
using UnityEngine;
//using System.Xml.Linq;

namespace Hearthstone
{
    public class Storage : MonoBehaviour, ISerialization
    {         
        private string _cardName = "DeckWarior";
        XmlDocument _xmlDoc;
        XmlElement _elemDeck;
        XmlElement _elemAreyIDCard;  

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
        }

        public void SaveDeck(List<int> _contentDeck)
        {
            for (int i = 0; i < _contentDeck.Count; i++)
            {
                //XmlElement elemIdCard = _xmlDoc.CreateElement($"id{_contentDeck[i]}");
                //_elemAreyIDCard.AppendChild(elemIdCard);
                Debug.Log($"Добавлена новая карта в колоду c ID{_contentDeck[i]}");
            }
            _xmlDoc.Save(_path);

        }
        public List<int> LoadDeck()
        {
            return null;
        }       

        /*private void SetDeck()
        {
            _elemAreyIDCard = _xmlDoc.CreateElement("AreyIDCard");
            _elemAreyIDCard.SetAttribute("Колличество", "30");
            _elemDeck.AppendChild(_elemAreyIDCard);
            _xmlDoc.Save(_path);
        }
         */    
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

namespace Hearthstone
{
    public class Saving_Controller : MonoBehaviour, ISerialization
    {
        private string _pathSaveDocument;
        XDocument _xDocument;
        XElement _xElementRoote;       


        private void Start()
        {
            _pathSaveDocument = Application.dataPath + "/SaveData.xml";
            _xDocument = new XDocument();
            _xElementRoote = new XElement("SaveDeck");
            _xDocument.Add(_xElementRoote);
        }

        public void SaveDeck(List<int> _contentDeck)
        {
            XElement newDeckElement = new XElement("NameDeck");
            XAttribute nameDeckElement = new XAttribute("Название", "Воин_Пират");
            newDeckElement.Add(nameDeckElement);
            _xElementRoote.Add(newDeckElement);
            for (int i = 0; i < _contentDeck.Count; i++)
            {
                XElement idElement = new XElement("IdCard", _contentDeck[i].ToString());
                newDeckElement.Add(idElement);
                //Debug.Log($"Добавлена новая карта в колоду c ID{_contentDeck[i]}");
            }                      
            _xDocument.Save(_pathSaveDocument);
        }

        public List<int> LoadDeck()
        {
            throw new System.NotImplementedException();
        }
    }
}
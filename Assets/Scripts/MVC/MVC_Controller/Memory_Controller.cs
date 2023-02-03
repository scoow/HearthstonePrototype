using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


namespace Hearthstone
{
    public class Memory_Controller : MonoBehaviour, ISave, IRemove
    {
        private ContentDeck_Model _contentDeck_Model;
        public CardCollectionSO_Model cardCollectionSO_Model;
        private DeckCollection_Controller _deckCollection_Controller;

        private string _pathSaveDocument ;
        XDocument _xDocument;
        XElement _xElementRoote;

        private void Awake()
        {
            _deckCollection_Controller = FindObjectOfType<DeckCollection_Controller>();
            _contentDeck_Model = FindObjectOfType<ContentDeck_Model>();
            _pathSaveDocument = Application.dataPath + "/SaveData.xml";
        }

        private void Start()
        {
            LoadDeckCollection();
            //_xDocument = new XDocument();
            //_xElementRoote = new XElement("SaveDeckCollection");
            //_xDocument.Add(_xElementRoote);
        }

        public void SaveDeck()
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement? saveDeckCollection = xdoc.Element("SaveDeckCollection");
            XElement deckNameElement = new XElement("DeckName");
            XAttribute nameAtribute = new XAttribute("NameValue", $"{_contentDeck_Model._currentDeckName}");
            deckNameElement.Add(nameAtribute);
            saveDeckCollection.Add(deckNameElement);
           // _xElementRoote.Add(deckNameElement);
            for (int i = 0; i < _contentDeck_Model._contentDeck.Count; i++)
            {               
                XElement idElement = new XElement("CardId",$"{_contentDeck_Model._contentDeck[i]}");
                deckNameElement.Add(idElement);
            }
            xdoc.Save(_pathSaveDocument);  
            
        }

        public void LoadDeckCollection()
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement? saveDeckCollection = xdoc.Element("SaveDeckCollection");
            if (saveDeckCollection is not null)
            {
                // проходим по всем элементам saveDeckCollection
                foreach (XElement deck in saveDeckCollection.Elements("DeckName"))
                {
                    XAttribute? nameDeck = deck.Attribute("NameValue");
                    _contentDeck_Model._currentDeckName = nameDeck.Value;
                    Debug.Log(_contentDeck_Model._currentDeckName);
                    _deckCollection_Controller.AddDeckInCollection();

                }
            }

        }

        public void DeleteDeckInCollection(string nameDeleteDeck)
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement? saveDeckCollection = xdoc.Element("SaveDeckCollection");
            if (saveDeckCollection is not null)
            {
                // проходим по всем элементам saveDeckCollection
                foreach (XElement deck in saveDeckCollection.Elements("DeckName"))
                {
                    XAttribute? nameDeck = deck.Attribute("NameValue");
                    if(nameDeck.Value == nameDeleteDeck)
                    {
                        deck.Remove();
                        Debug.Log($"Из коллекции удалена колода {nameDeck.Value}");
                    }
                }
                xdoc.Save(_pathSaveDocument);
            }
        }
    }
}
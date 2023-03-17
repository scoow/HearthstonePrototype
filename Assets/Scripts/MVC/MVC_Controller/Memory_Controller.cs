using System;
using System.Xml.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class Memory_Controller : MonoBehaviour, ISave, IRemove , IActive
    {
        private ContentDeck_Model _contentDeck_Model;
        private PageBook_Model _pageBook_Model;        
        private DeckCollection_Controller _deckCollection_Controller;
        private string _pathSaveDocument ;        

        private void Awake()
        {
            _deckCollection_Controller = FindObjectOfType<DeckCollection_Controller>();
            _contentDeck_Model = FindObjectOfType<ContentDeck_Model>();
            _pathSaveDocument = Application.dataPath + "/SaveData.xml";
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
        }

        private void Start()
        {
            LoadDeckCollection();            
        }

        public void SaveDeck()
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement saveDeckCollection = xdoc.Element("SaveDeckCollection");
            XElement deckNameElement = new XElement("DeckName");
            XAttribute nameAtribute = new XAttribute("NameValue", $"{_contentDeck_Model.CurrentDeckName}");
            XAttribute stateAtribute = new XAttribute("StateValue", "Неактивная колода");
            deckNameElement.Add(nameAtribute);
            deckNameElement.Add(stateAtribute);
            saveDeckCollection.Add(deckNameElement);

            for (int i = 0; i < _contentDeck_Model.ContentDeck.Count; i++)
            {
                XElement idElement = new XElement("CardId",$"{_contentDeck_Model.ContentDeck[i]}");
                deckNameElement.Add(idElement);
            }
            xdoc.Save(_pathSaveDocument);  
            
        }

        public void LoadDeckCollection()
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement saveDeckCollection = xdoc.Element("SaveDeckCollection");
            if (saveDeckCollection is not null)
            {
                // проходим по всем элементам saveDeckCollection
                foreach (XElement deck in saveDeckCollection.Elements("DeckName"))
                {
                    XAttribute nameDeck = deck.Attribute("NameValue");
                    _contentDeck_Model.CurrentDeckName = nameDeck.Value;                    
                    _deckCollection_Controller.AddDeckInCollection();
                }
            }
        }

        public void DeleteDeckInCollection(string nameDeleteDeck)
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement saveDeckCollection = xdoc.Element("SaveDeckCollection");
            if (saveDeckCollection is not null)
            {
                // проходим по всем элементам saveDeckCollection
                foreach (XElement deck in saveDeckCollection.Elements("DeckName"))
                {
                    XAttribute nameDeck = deck.Attribute("NameValue");
                    if(nameDeck.Value == nameDeleteDeck)
                    {
                        deck.Remove();                       
                    }
                }
                xdoc.Save(_pathSaveDocument);
            }
        }

        public void ChangeStateDeck(string nameActiveDeck)
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement saveDeckCollection = xdoc.Element("SaveDeckCollection");
            if (saveDeckCollection is not null)
            {
                // проходим по всем элементам saveDeckCollection и обнуляем состояние колод
                foreach (XElement deck in saveDeckCollection.Elements("DeckName"))
                {
                    XAttribute stateDeck = deck.Attribute("StateValue");
                    if (stateDeck.Value == "Выбранная колода")
                    {
                        stateDeck.Value = "Неактивная колода";
                    }
                }
                // проходим по всем элементам saveDeckCollection и устанавливаем активный статус для выбранной колоды
                foreach (XElement deck in saveDeckCollection.Elements("DeckName"))
                {
                    XAttribute nameDeck = deck.Attribute("NameValue");
                    if (nameDeck.Value == nameActiveDeck)
                    {
                        XAttribute stateDeck = deck.Attribute("StateValue");
                        stateDeck.Value = "Выбранная колода";
                    }                 
                }
                xdoc.Save(_pathSaveDocument);
            }
        }
    }
}
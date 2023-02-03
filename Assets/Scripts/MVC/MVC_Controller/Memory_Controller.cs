using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


namespace Hearthstone
{
    public class Memory_Controller : MonoBehaviour, ISave
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
        }

        private void Start()
        {
            _pathSaveDocument = Application.dataPath + "/SaveData.xml";
            XDocument _xDocument = XDocument.Load(_pathSaveDocument);
            XElement _xElementRoote = _xDocument.Element("SaveDeckCollection");
            LoadDeck();
            
            
            //_xDocument = new XDocument();
            //_xElementRoote = new XElement("SaveDeckCollection");
            //_xDocument.Add(_xElementRoote);
            
              
        }

        public void SaveDeck()
        {
            XElement deckNameElement = new XElement("DeckName");
            XAttribute nameAtribute = new XAttribute("NameValue", $"{_contentDeck_Model._currentDeckName}");
            deckNameElement.Add(nameAtribute);
            _xElementRoote.Add(deckNameElement);
           // _xElementRoote.Add(deckNameElement);
            for (int i = 0; i < _contentDeck_Model._contentDeck.Count; i++)
            {               
                XElement idElement = new XElement("CardId",$"{_contentDeck_Model._contentDeck[i]}");
                deckNameElement.Add(idElement);
            }
            _xDocument.Save(_pathSaveDocument);  
            
        }
        
        
        /// <summary>
        /// загружаем XDocument
        /// </summary>
        /// <returns></returns>
        /*public XDocument LoadXDocumentInXml()
        {
            _xDocument = XDocument.Load(Application.dataPath + "/SaveData.xml");
            return _xDocument;
        }*/

        /*public void AddInDeck()
        {
            _xDocument.Add
        }*/

        public void LoadDeck()
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
    }

}
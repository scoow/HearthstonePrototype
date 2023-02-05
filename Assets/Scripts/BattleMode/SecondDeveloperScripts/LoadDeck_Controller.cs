using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace Hearthstone
{
    public class LoadDeck_Controller : MonoBehaviour
    {
        private int _heroId;
        [SerializeField]
        private Image _heroSprite;
        public List<int> _activeDeck = new List<int>();
        private string _pathSaveDocument;
        private PageBook_Model _pageBookModel;       
        private MulliganManager _mulliganManager;

        private void Awake()
        {
            _mulliganManager = FindObjectOfType<MulliganManager>();
            _pageBookModel = GetComponent<PageBook_Model>();
            _pathSaveDocument = Application.dataPath + "/SaveData.xml";
        }
        private void Start()
        {           
            LoadActiveDeck();
            CardSettings_Model[] _temporaryArray = _mulliganManager.transform.GetComponentsInChildren<CardSettings_Model>();
            for(int i = 0; i <= _temporaryArray.Length-1; i++)
            {               
                LoadCardSettings(_temporaryArray[i], _activeDeck[i]);
                LayerRenderUp layerRenderUp = _temporaryArray[i].GetComponent<LayerRenderUp>();
                layerRenderUp.LayerUp(i);
                if (i == _temporaryArray.Length - 1) layerRenderUp.LayerLastSpriteUp();
                //_temporaryArray[i].GetComponent<ChoiseCard_View>().ChangeViewCard(_temporaryArray[i], _activeDeck[i]);
                //_temporaryArray[i] = _pageBookModel._cardsDictionary[_activeDeck[i]] ;
            }
            /*foreach (CardSettings_Model child in _temporaryArray)
            {
                if (child.TryGetComponent(out EmissionMarker image))
                {
                    image.gameObject.GetComponent<Image>().color = new Color(172, 172, 172, 255);
                }
            }*/
        }

        public void LoadCardSettings(CardSettings_Model settingsGameCard, int cardId)
        {
            CardSO_Model cardSettings = _pageBookModel._cardsDictionary[cardId];
            settingsGameCard.ManaCost.text = cardSettings._manaCostCard.ToString();
            settingsGameCard.Name.text = cardSettings._nameCard;
            settingsGameCard.AtackDamage.text = cardSettings._atackDamageCard.ToString();
            settingsGameCard.Healt.text = cardSettings._healtCard.ToString();
            settingsGameCard.Description.text = cardSettings._descriptionCard;
            settingsGameCard.SpriteCard.sprite = cardSettings._spriteCard;
        }



        public void LoadActiveDeck()
        {
            XDocument xdoc = XDocument.Load(_pathSaveDocument);
            XElement saveDeckCollection = xdoc.Element("SaveDeckCollection");
            if (saveDeckCollection is not null)
            {                
                foreach (XElement deck in saveDeckCollection.Elements("DeckName"))
                {
                    XAttribute stateDeck = deck.Attribute("StateValue");
                    if (stateDeck.Value == "Выбранная колода")
                    {
                        foreach(XElement cardId in deck.Elements("CardId"))
                        {
                            if (Int32.Parse(cardId.Value) >= 1000)
                            {
                                _heroId = Int32.Parse(cardId.Value);
                                foreach (CardSO_Model cardSO in _pageBookModel.cardCollectionSO_Model._heroCollection)
                                {
                                    if(cardSO._idCard == Int32.Parse(cardId.Value)) _heroSprite.sprite = cardSO._spriteCard;
                                    continue;
                                }                                
                                continue;
                            }                               
                            _activeDeck.Add(Int32.Parse(cardId.Value));                            
                        }
                    }
                }
            }
        }
    }
}
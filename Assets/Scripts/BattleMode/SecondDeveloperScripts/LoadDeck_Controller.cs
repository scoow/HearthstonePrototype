using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Hearthstone
{
    public class LoadDeck_Controller : MonoBehaviour
    {
        private int _heroId;
        [SerializeField]
        private Image _heroSprite;
        /// <summary>
        /// активна€ колода в игре
        /// </summary>
        public List<int> _activeDeck = new List<int>();
        private string _pathSaveDocument;
        private PageBook_Model _pageBookModel;
     /*   [Inject]
        private MulliganManager _mulliganManager;*/
        public Action SetCardSettings;
        public Action<int> SetHeroSettings;

        private void Awake()
        {
            _pageBookModel = GetComponent<PageBook_Model>();
            _pathSaveDocument = Application.dataPath + "/SaveData.xml";
        }
        private void Start()
        {           
            LoadActiveDeck();            
            ShuffleCardInDeck();
            //Card_Model[] _temporaryArray = _mulliganManager.transform.GetComponentsInChildren<Card_Model>();
            Card_Model[] _temporaryArray = FindObjectsOfType<Card_Model>();
            for (int i = 0; i <= _temporaryArray.Length-1; i++)
            {               
                LoadCardSettings(_temporaryArray[i], _activeDeck[i]);                
                _temporaryArray[i].SetCardSettings(_activeDeck[i], false);                
                LayerRenderUp layerRenderUp = _temporaryArray[i].GetComponent<LayerRenderUp>();
                layerRenderUp.LayerUp(i);
                //if (i == _temporaryArray.Length - 1) layerRenderUp.LayerLastSpriteUp();               
            }            
        }

        public void LoadCardSettings(Card_Model card_Model, int cardId)
        {
            CardSO_Model cardSO_Model = (CardSO_Model)_pageBookModel._cardsDictionary[cardId];
            card_Model._atackDamageCard = cardSO_Model._atackDamageCard;
            card_Model._descriptionCard = cardSO_Model._descriptionCard;
            card_Model._manaCostCard = cardSO_Model._manaCostCard;
            card_Model._healthCard = cardSO_Model._healthCard;
            card_Model._spriteCard = cardSO_Model._spriteCard;
            card_Model._nameCard = cardSO_Model._nameCard;       
                       
            SetCardSettings?.Invoke();
        }
        private void ShuffleCardInDeck()
        {           
            int maxIndex = _activeDeck.Count;
            while (maxIndex > 1)
            {
                maxIndex--;
                int randomIndex = UnityEngine.Random.Range(0,maxIndex);
                int value = _activeDeck[randomIndex];
                _activeDeck[randomIndex] = _activeDeck[maxIndex];
                _activeDeck[maxIndex] = value;                
            }            
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
                    if (stateDeck.Value == "¬ыбранна€ колода")
                    {
                        foreach(XElement cardId in deck.Elements("CardId"))
                        {
                            if (Int32.Parse(cardId.Value) >= 1000)
                            {
                                _heroId = Int32.Parse(cardId.Value);
                                foreach (GameSO_Model cardSO in _pageBookModel.cardCollectionSO_Model._heroCollection)
                                {
                                    if(cardSO._idCard == Int32.Parse(cardId.Value))
                                    {
                                        _heroSprite.sprite = cardSO._spriteCard;
                                        SetHeroSettings?.Invoke(_heroId);
                                        continue;
                                    }                                    
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
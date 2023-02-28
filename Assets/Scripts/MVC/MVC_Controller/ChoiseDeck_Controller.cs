using UnityEngine;

namespace Hearthstone
{
    public class ChoiseDeck_Controller : MonoBehaviour
    {
        private CreateDeck_Marker _createDeck_Marker;
        private ChoiseHeroMarker _choiseHero_Marker;
        private DeckCollection_Marker _deckCollection_Marker;
        private DeckCreate_Controller _deckCreateController;
        private PageBook_Model _pageBook_Model;
        private PageBook_View _pageBook_View;
        private PageBook_Controller _pageBook_Controller;

        private void Awake()
        {
            _deckCollection_Marker = FindObjectOfType<DeckCollection_Marker>();
            _deckCreateController = FindObjectOfType<DeckCreate_Controller>();
            _createDeck_Marker = GetComponentInChildren<CreateDeck_Marker>();
            _choiseHero_Marker = GetComponentInChildren<ChoiseHeroMarker>();
            _pageBook_Controller = GetComponent<PageBook_Controller>();
            _pageBook_Model = GetComponent<PageBook_Model>();
            _pageBook_View = GetComponent<PageBook_View>();
        }

        private void Start()
        {
            ChoiseDeck();
        }

        public void ChoiseDeck()
        {
            _pageBook_Model._createDeckState = CreateDeckState.ChoiseDeck;
            ChangeOFState(_pageBook_Model._createDeckState);
        }
        public void CreateDeck()
        {
            _pageBook_Model._createDeckState = CreateDeckState.CreateDeck;
            ChangeOFState(_pageBook_Model._createDeckState);
            _pageBook_View.UpdatePageBook(_pageBook_Model._resultCollection, _pageBook_Controller._cardTemplatePrefabs, _pageBook_Controller._pageCounter);

        }

        public void ChoiseHero()
        {
            _pageBook_Model._createDeckState = CreateDeckState.ChoiseHero;
            ChangeOFState(_pageBook_Model._createDeckState);
        }


        private void ChangeOFState(CreateDeckState createDeckState)
        {
            switch (createDeckState)
            {
                case CreateDeckState.ChoiseHero://
                    _choiseHero_Marker.gameObject.SetActive(true);
                    _createDeck_Marker.gameObject.SetActive(false);
                    break;
                case CreateDeckState.ChoiseDeck://
                    _choiseHero_Marker.gameObject.SetActive(false);
                    _createDeck_Marker.gameObject.SetActive(true);
                    _deckCreateController.gameObject.SetActive(false);
                    _deckCollection_Marker.gameObject.SetActive(true);
                    break;
                case CreateDeckState.CreateDeck://
                    _choiseHero_Marker.gameObject.SetActive(false);
                    _createDeck_Marker.gameObject.SetActive(true);
                    _deckCreateController.gameObject.SetActive(true);
                    _deckCollection_Marker.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
using Hearthstone;
using UnityEngine;

public class ChoiseDeck_Controller : MonoBehaviour
{
    private CreateDeck_Marker _createDeck_Marker;
    private ChoiseHeroMarker _choiseHero_Marker;
    private DeckCollection_Controller _deckCollectionController;
    private DeckCreate_Controller _deckCreateController;

    private PageBook_Model _pageBookModel;

    private void Awake()
    {
        _deckCollectionController = FindObjectOfType<DeckCollection_Controller>();
        _deckCreateController = FindObjectOfType<DeckCreate_Controller>();
        _createDeck_Marker = GetComponentInChildren<CreateDeck_Marker>();
        _choiseHero_Marker = GetComponentInChildren<ChoiseHeroMarker>();
        _pageBookModel = GetComponent<PageBook_Model>();
    }

    private void Start()
    {
        ChangeWiew();
    }

    public void ChangeWiew()
    {
        if(_choiseHero_Marker.gameObject.activeInHierarchy)
        {
            _createDeck_Marker.gameObject.SetActive(true);
            _deckCreateController.gameObject.SetActive(true);
            //_deckCollectionController.gameObject.SetActive(false);
            _choiseHero_Marker.gameObject.SetActive(false);
            
            return;
        }
        if(_createDeck_Marker.gameObject.activeInHierarchy)
        {
            _choiseHero_Marker.gameObject.SetActive(true);
            _deckCreateController.gameObject.SetActive(true);
            _createDeck_Marker.gameObject.SetActive(false);
            _deckCollectionController.gameObject.SetActive(false);
            
            _pageBookModel._createDeckState = CreateDeckState.CreateDeck;
            return ;
        }
    }
}
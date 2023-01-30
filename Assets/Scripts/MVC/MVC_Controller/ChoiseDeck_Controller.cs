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
        ChoiseDeck();        
    }

    public void ChoiseDeck() => ChangeOFState(CreateDeckState.ChoiseDeck);   
    public void CreateDeck() => ChangeOFState(CreateDeckState.CreateDeck);
    public void ChoiseHero() => ChangeOFState(CreateDeckState.ChoiseHero);  

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
                _deckCollectionController.gameObject.SetActive(true);                
                break;
            case CreateDeckState.CreateDeck://
                _choiseHero_Marker.gameObject.SetActive(false);
                _createDeck_Marker.gameObject.SetActive(true);
                _deckCreateController.gameObject.SetActive(true);
                _deckCollectionController.gameObject.SetActive(false);                
                break;
            default:
                break;
        }


    }
}
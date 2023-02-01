using Hearthstone;
using UnityEngine;

public class ChoiseDeck_Controller : MonoBehaviour
{
    private CreateDeck_Marker _createDeck_Marker;
    private ChoiseHeroMarker _choiseHero_Marker;
    private DeckCollection_Marker _deckCollection_Marker;
    private DeckCreate_Controller _deckCreateController;
    private PageBook_Model _pageBookModel;

    private void Awake()
    {
        _deckCollection_Marker = FindObjectOfType<DeckCollection_Marker>();
        _deckCreateController = FindObjectOfType<DeckCreate_Controller>();
        _createDeck_Marker = GetComponentInChildren<CreateDeck_Marker>();
        _choiseHero_Marker = GetComponentInChildren<ChoiseHeroMarker>();
        _pageBookModel = GetComponent<PageBook_Model>();
    }

    private void Start()
    {
        ChoiseDeck();        
    }

    public void ChoiseDeck()
    {
        _pageBookModel._createDeckState = CreateDeckState.ChoiseDeck;
        ChangeOFState(_pageBookModel._createDeckState);
    }      
    public void CreateDeck()
    {
        _pageBookModel._createDeckState = CreateDeckState.CreateDeck;
        ChangeOFState(_pageBookModel._createDeckState);
    }
    
    public void ChoiseHero()
    {
        _pageBookModel._createDeckState = CreateDeckState.ChoiseHero;
        ChangeOFState(_pageBookModel._createDeckState);
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
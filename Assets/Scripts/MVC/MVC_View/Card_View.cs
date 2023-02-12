using Hearthstone;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// класс содержит метод реализующий замену текущих настроек карты на новые 
/// </summary>
public class Card_View : MonoBehaviour, IChange
{
    private PageBook_View _pageBook_View;
    private PageBook_Model _pageBook_Model;
    private LoadDeck_Controller _loadDeck_Controller;
    private SpriteRenderer _spriteCard;
    private Card_Model _card_Model;
    private int _id;
    private Text _nameText;
    private Text _descriptionText;   
    private Text _manaCostText;
    private Text _atackDamageText;
    private Text _healtText;    
    

    public Text Name { get => _nameText; }
    public Text Description { get => _descriptionText; }
    public int Id { get => _id; set => _id = value; }
    public SpriteRenderer SpriteCard { get => _spriteCard; }
    public Text ManaCost { get => _manaCostText; }
    public Text AtackDamage { get => _atackDamageText; set => _atackDamageText = value; }
    public Text Healt { get => _healtText; set => _healtText = value; }

    private void OnEnable()
    {
        _card_Model = GetComponent<Card_Model>();        
        _pageBook_Model = FindObjectOfType<PageBook_Model>();
        if (gameObject.GetComponent<BattleModeCard>() != null)
        {
            _loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();
            _loadDeck_Controller.SetSettings += ChangeViewCard;
        }
        if(gameObject.GetComponent<CreateModeCard>() != null)
        {
            _pageBook_View = FindObjectOfType<PageBook_View>();
            _pageBook_View.OnUpdatePageBook += ChangeViewCard;
        }    

    }
    private void OnDisable()
    {
        if (gameObject.GetComponent<BattleModeCard>() != null)
            _loadDeck_Controller.SetSettings -= ChangeViewCard;
        if (gameObject.GetComponent<CreateModeCard>() != null)
            _pageBook_View.OnUpdatePageBook -= ChangeViewCard;
    }



    private void Awake()
    {
        if (gameObject.tag == "CardTemplate" || gameObject.tag == "CardZooming")
        {
            _descriptionText = GetComponentInChildren<TextDiscriptionMarker>().GetComponent<Text>();
            _atackDamageText = GetComponentInChildren<TextAtackMarker>().GetComponent<Text>();
            _healtText = GetComponentInChildren<TextHealthMarker>().GetComponent<Text>();
            _spriteCard = GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>();
        }
        _nameText = GetComponentInChildren<TextCardNameMarker>().GetComponent<Text>();
        _manaCostText = GetComponentInChildren<TextManaCostMarker>().GetComponent<Text>();        
    }
   


    public void ChangeViewCard()
    {
        if (gameObject.tag == "CardTemplate" )
        {
            _descriptionText.text = _card_Model._descriptionCard;
            _atackDamageText.text = _card_Model._atackDamageCard.ToString();
            _healtText.text = _card_Model._healthCard.ToString();
            _spriteCard.sprite = _card_Model._spriteCard;
        }
        _nameText.text = _card_Model._nameCard;        
        _manaCostText.text = _card_Model._manaCostCard.ToString();        
    }

    public void ChangeViewZomingCard(int idCard)
    {
        CardSO_Model cardSO = _pageBook_Model._cardsDictionary[idCard];        
        _descriptionText.text = cardSO._descriptionCard;
        _atackDamageText.text = cardSO._atackDamageCard.ToString();
        _healtText.text = cardSO._healthCard.ToString();
        _spriteCard.sprite = cardSO._spriteCard;        
        _nameText.text = cardSO._nameCard;
        _manaCostText.text = cardSO._manaCostCard.ToString();
    }
}
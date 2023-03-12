using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEffect_Controller : MonoBehaviour
{
    private Mana_Controller _manaController;
    private PageBook_Model _pageBook_Model;
    //private int _lastEffect;
    [SerializeField] private Transform _playerFirstBoard;
    [SerializeField] private Transform _playerSecondBoard;


    /// <summary>
    /// список эффектов по событию
    /// </summary>
    public List<int> _activeEventEffect;

    private void OnEnable()
    {
        _pageBook_Model = FindObjectOfType<PageBook_Model>();
        _manaController = FindObjectOfType<Mana_Controller>();
        _manaController.OnChangeTurn += ParseChangeTurnEvent;
    }

    private void OnDisable()
    {
        _manaController.OnChangeTurn -= ParseChangeTurnEvent;
    }

    public void AddEventEffect(int cardId) //добавить эффект в список
    {
        _activeEventEffect.Add(cardId);
    }

    public void RemoveEventEffect(int cardId) //удалить эффект из списка
    {
        _activeEventEffect.Remove(cardId);
    }


    /// <summary>
    /// обработка события лечения
    /// </summary>
    /// <param name="cardExample"></param>
    public void ParseHealEvent(ApplyBattleCry cardExample)
    {

        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect)
            {   
                if (cardEffectId == 106)
                {
                    cardExample.gameObject.GetComponent<Card_Controller>().TakeAdditionalCard();
                }             
            }
        }
    }

    /// <summary>
    /// обработка события урона
    /// </summary>
    public void ParseDamageEvent()
    {
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе первого игрока
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе второго игрока

        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //обхожу весь список евент еффектов
            {
                if (cardEffectId == 310) // если есть такой эффект
                {
                    foreach(Card_Controller cardController in _cardPlayerFirst)
                    {
                        if(cardController.GetComponent<Card_Model>()._idCard == cardEffectId) //то вызываю метод Берсерк у карты с таким же Id
                        {
                            cardController.BerserkAbility();
                        }
                    }                    
                }
            }
        }
    }

    /// <summary>
    /// обработка события смерти
    /// </summary>
    public void ParseDeathEvent(Card_Controller cardExample)
    {
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе первого игрока
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе второго игрока
        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //тип миньона вызвавшего событие
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //обхожу весь список евент еффектов
            {
                if (cardEffectId == 212) // если есть такой эффект
                {
                    foreach (Card_Controller cardController in _cardPlayerFirst)
                    { 
                        Card_Model cardModel = cardController.GetComponent<Card_Model>();
                        if (cardModel._idCard == cardEffectId && cardModel._minionType == incomingMinionType) //то вызываю метод увеличения параметров
                        {
                            cardController.ChangeAtackValue(cardModel._changeAtackValue);
                            cardController.ChangeHealtValue(cardModel._сhangeHealthValue);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// обработка события установки карты на доску
    /// </summary>
    public void ParsePutCardInBoard(Card_Controller cardExample)
    {
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе первого игрока
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе второго игрока

        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //тип миньона вызвавшего событие
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //обхожу весь список евент еффектов
            {
                if (cardEffectId == 213) // если есть такой эффект
                {
                    foreach (Card_Controller cardController in _cardPlayerFirst)
                    {
                        Card_Model cardModel = cardController.GetComponent<Card_Model>();
                        if (cardModel._idCard == cardEffectId && cardModel._minionType == incomingMinionType && cardModel.gameObject != cardExample.gameObject)
                        {
                            cardController.TakeAdditionalCard();
                        }
                    }
                }

                if (cardEffectId == 501)
                {
                    CardSO_Model card_Model = (CardSO_Model)_pageBook_Model._cardsDictionary[cardEffectId];
                    foreach (Card_Controller cardController in _cardPlayerFirst)
                    {
                        cardController.ChangeHealtValue(card_Model._abilityChangeHealth,ChangeHealthType.Healing);                        
                    }
                }

            }
            /*
             ставим новую карту
            вызвать событие - если активен эффект 213 и это зверь, то взять карту
             */
        }
    }


    public void ParseChangeTurnEvent(Players playersTurn)
    {
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе первого игрока
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе второго игрока
        if (_cardPlayerFirst == null) return;
        

        foreach(Card_Controller cardModel1 in _cardPlayerFirst)
        {     
            Card_Model card_Model1 = cardModel1.GetComponent<Card_Model>();
            if (card_Model1._idCard == 211 && ((playersTurn == Players.First && card_Model1.transform.parent == _playerFirstBoard) || (playersTurn == Players.Second && card_Model1.transform.parent == _playerSecondBoard)))
            {

                List<Card_Model> tempListCardModel = new List<Card_Model>(); //временный лист раненых
                foreach (Card_Controller cardModel2 in _cardPlayerFirst)
                {
                    Card_Model card_Model2 = cardModel2.GetComponent<Card_Model>();
                    if (card_Model2._healthCard < card_Model2._maxHealtValue)
                    {
                        tempListCardModel.Add(card_Model2);
                    }
                    
                }
                int indexWounded = Random.Range(0, tempListCardModel.Count); //выбираем счастливчика
                tempListCardModel[indexWounded].GetComponent<Card_Controller>().ChangeHealtValue(card_Model1._сhangeHealthValue, ChangeHealthType.Healing); //лечим его

            }
        }
        
    }
}
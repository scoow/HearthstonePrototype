using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEffect_Controller : MonoBehaviour
{
    private PageBook_Model _pageBook_Model;
    //private int _lastEffect;
    [SerializeField] private Transform _playerBoard;

    /// <summary>
    /// список эффектов по событию
    /// </summary>
    public List<int> _activeEventEffect;

    private void OnEnable()
    {
        _pageBook_Model = FindObjectOfType<PageBook_Model>();
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
        Card_Controller[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Controller>();

        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //обхожу весь список евент еффектов
            {
                if (cardEffectId == 310) // если есть такой эффект
                {
                    foreach(Card_Controller cardController in _cardControllerArray)
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
        Card_Controller[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе
        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //тип миньона вызвавшего событие
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //обхожу весь список евент еффектов
            {
                if (cardEffectId == 212) // если есть такой эффект
                {
                    foreach (Card_Controller cardController in _cardControllerArray)
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
        Card_Controller[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Controller>(); //все карты на столе
        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //тип миньона вызвавшего событие
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //обхожу весь список евент еффектов
            {
                if (cardEffectId == 213) // если есть такой эффект
                {
                    foreach (Card_Controller cardController in _cardControllerArray)
                    {
                        Card_Model cardModel = cardController.GetComponent<Card_Model>();
                        if (cardModel._idCard == cardEffectId && cardModel._minionType == incomingMinionType && cardModel.gameObject != gameObject)
                        {
                            cardController.TakeAdditionalCard();
                        }
                    }
                }
            }
        }
    }
}
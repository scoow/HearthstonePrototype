using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PermanentEffect_Controller : MonoBehaviour
{
    private PageBook_Model _pageBook_Model;
    private int _lastEffect;
    [SerializeField] private Transform _playerBoard;


    /// <summary>
    /// список постоянных эффектов
    /// </summary>
    public List<int> _activePermanentEffect;

    private void OnEnable()
    {
        _pageBook_Model = FindObjectOfType<PageBook_Model>();
    }


    public void AddPermanentEffect(int cardId) //добавить эффект в список и применить его на поле
    {
        //_activePermanentEffect.Add(cardId);        
        AceptPermanentEffect(cardId);
    }

    public void RemovePermanentEffect(int cardId) //отменить эффект с карт и удалить его из списка
    {
        CardSO_Model cardSO_Model = _pageBook_Model._cardsDictionary[cardId];
        UpdatePermanentEffect(cardId, -cardSO_Model._abilityChangeHealth, -cardSO_Model._abilityChangeAtack);
        _activePermanentEffect.Remove(cardId);
        
    }
    private void AceptPermanentEffect(int cardId) //применить эффект на картах которые стоят на поле
    {
        CardSO_Model cardSO_Model = _pageBook_Model._cardsDictionary[cardId];
        UpdatePermanentEffect(cardId, cardSO_Model._abilityChangeHealth, cardSO_Model._abilityChangeAtack); //обновляем значения карт
        _activePermanentEffect.Add(cardId);                                                                                                   
    }


    


    private void UpdatePermanentEffect(int cardEffectId , int changeHealthValue, int changeAtackValue)
    {
        CardSO_Model cardSO_Model = _pageBook_Model._cardsDictionary[cardEffectId];
        Card_Controller[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Controller>();
        for (int i = 0; i < _cardControllerArray.Length; i++)
        {
            Card_Model currentCard = _cardControllerArray[i].gameObject.GetComponent<Card_Model>();
            if (currentCard._idCard == cardEffectId) continue;

            if((cardEffectId == 102 || cardEffectId == 107) && (cardSO_Model._targetsType == currentCard._minionType)) //баф карт 102 и 107
            {
                _cardControllerArray[i].ChangeAtackValue(changeAtackValue);
            }            
        }
    }

    public void GetActivePermanentEffect(Card_Controller cardController) //берём уже действующий эффект на столе
    {    
        Card_Model currentCard = cardController.GetComponent<Card_Model>();
        if (_activePermanentEffect != null)
        {
            foreach(int cardEffectId in _activePermanentEffect)
            {
                if (currentCard._idCard == cardEffectId) continue;

                CardSO_Model cardSO_Model = _pageBook_Model._cardsDictionary[cardEffectId];
                if ((cardEffectId == 102 || cardEffectId == 107) && (cardSO_Model._targetsType == currentCard._minionType)) //баф карт 102 и 107
                {
                    cardController.ChangeAtackValue(cardSO_Model._abilityChangeAtack);
                }

                if (cardEffectId == 304)
                {
                    cardController.ChangeAtackValue(cardSO_Model._abilityChangeAtack);
                }

                if (cardEffectId == 309 && cardSO_Model._atackDamageCard <= 3)
                {
                    cardController.ChargeAbility();
                }

                if(cardEffectId == 505 && (cardSO_Model._targetsType == currentCard._minionType))
                {
                    cardController.ChargeAbility();
                }
            }
        }
    }    
}
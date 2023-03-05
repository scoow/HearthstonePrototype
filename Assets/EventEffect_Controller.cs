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
}
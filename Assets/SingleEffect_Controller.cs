using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class SingleEffect_Controller : MonoBehaviour
{
    private PermanentEffect_Controller _permanentEffect_Controller;
    private PageBook_Model _pageBook_Model;
    [SerializeField] private Transform _playerBoard;

    private void OnEnable()
    {
        _permanentEffect_Controller = GetComponent<PermanentEffect_Controller>();
        _pageBook_Model = GetComponent<PageBook_Model>();
    }

    public void ApplyEffect(ApplyBattleCry card)
    {        
        Card_Model cardModel = card.gameObject.GetComponent<Card_Model>();
        Card_Controller cardController = card.gameObject.GetComponent<Card_Controller>();
        Card incomingCard = card.GetComponent<Card>();
        Card_Model[] _temporaryArray = _playerBoard.GetComponentsInChildren<Card_Model>();
        if (cardModel._idCard == 503)
        {
            cardController.ChangeAtackValue(_temporaryArray.Length);
            cardController.ChangeHealtValue(_temporaryArray.Length);
        }
        
        List<int> activePermanentEffect;
        if (incomingCard._side == Players.First)
            activePermanentEffect = _permanentEffect_Controller._activePermanentEffectPlayersFirst;
        else
            activePermanentEffect = _permanentEffect_Controller._activePermanentEffectPlayersSecond;
        if (cardModel._idCard == 407 && activePermanentEffect.Contains(cardModel._idCard))
        {            
            BattleModeCard_View card_View = card.GetComponent<BattleModeCard_View>();
            cardModel._atackDamageCard = cardModel._healthCard;
            card_View.UpdateViewCard();
        }
    } 
}
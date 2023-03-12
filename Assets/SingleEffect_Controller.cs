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

    public void ApplyEffect(Card_Controller cardExample)
    {
        
        int cardId = cardExample.gameObject.GetComponent<Card_Model>()._idCard;
        Card_Model[] _temporaryArray = _playerBoard.GetComponentsInChildren<Card_Model>();
        if (cardId == 503)
        {            
            cardExample.ChangeAtackValue(_temporaryArray.Length);
            cardExample.ChangeHealtValue(_temporaryArray.Length);
        }



        //как-то переделать покрасивее
        Card incomingCard = cardExample.GetComponent<Card>();
        List<int> activePermanentEffect;
        if (incomingCard._side == Players.First)
            activePermanentEffect = _permanentEffect_Controller._activePermanentEffectPlayersFirst;
        else
            activePermanentEffect = _permanentEffect_Controller._activePermanentEffectPlayersSecond;
        if (cardId == 407 && activePermanentEffect.Contains(cardId))
        {
            Card_Model card_Model = cardExample.GetComponent<Card_Model>();
            BattleModeCard_View card_View = cardExample.GetComponent<BattleModeCard_View>();
            card_Model._atackDamageCard = card_Model._healthCard;
            card_View.UpdateViewCard();
        }
    } 
}
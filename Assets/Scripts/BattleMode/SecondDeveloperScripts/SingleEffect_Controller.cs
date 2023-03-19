using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Hearthstone
{
    public class SingleEffect_Controller : MonoBehaviour
    {
        private PermanentEffect_Controller _permanentEffect_Controller;
        //private PageBook_Model _pageBook_Model;
        //[SerializeField] private Transform _playerBoard;//ИСПРАВИТЬ
        [Inject(Id = "First")]
        private Board _firstPlayerBoard;
        [Inject(Id = "Second")]
        private Board _secondPlayerBoard;
        [Inject(Id = "First")]
        private Hero_Controller _firstPlayerHero;
        [Inject(Id = "Second")]
        private Hero_Controller _secondPlayerHero;

        private void OnEnable()
        {
            _permanentEffect_Controller = GetComponent<PermanentEffect_Controller>();
            // _pageBook_Model = GetComponent<PageBook_Model>();
        }

        public void ApplyEffect(ApplyBattleCry card)
        {
            Card_Model cardModel = card.gameObject.GetComponent<Card_Model>();
            Card_Controller cardController = card.gameObject.GetComponent<Card_Controller>();
            Card incomingCard = card.GetComponent<Card>();

            Board _currentBoard = null;
            switch (incomingCard.GetSide())
            {
                case Players.First:
                    _currentBoard = _firstPlayerBoard;
                    break;
                case Players.Second:
                    _currentBoard = _secondPlayerBoard;
                    break;
                default:
                    break;
            }

            Card_Model[] _temporaryArray = _currentBoard.GetComponentsInChildren<Card_Model>();
            if (cardModel.IdCard == 503)
            {
                cardController.ChangeAtackValue(_temporaryArray.Length - 1);
                cardController.ChangeHealthValue(_temporaryArray.Length - 1);
            }
            if (cardModel.IdCard == 504)
            {
                Hero_Controller hero = null;
                switch (incomingCard.GetSide())
                {
                    case Players.First:
                        hero = _secondPlayerHero; 
                        break;
                    case Players.Second:
                        hero = _firstPlayerHero;
                        break;
                    default:
                        break;
                }
                hero.ChangeHealthValue(3, ChangeHealthType.DealDamage);//todo заменить 3 на значение из клича

            }

            List<int> activePermanentEffect;
            if (incomingCard._side == Players.First)
                activePermanentEffect = _permanentEffect_Controller._activePermanentEffectPlayersFirst;
            else
                activePermanentEffect = _permanentEffect_Controller._activePermanentEffectPlayersSecond;
            if (cardModel.IdCard == 407 && activePermanentEffect.Contains(cardModel.IdCard))
            {
                BattleModeCard_View card_View = card.GetComponent<BattleModeCard_View>();
                cardModel.AtackDamageCard = cardModel.HealthCard;
                card_View.UpdateViewCard();
            }
        }
    }
}
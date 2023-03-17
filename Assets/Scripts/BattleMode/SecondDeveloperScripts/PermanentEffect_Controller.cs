using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class PermanentEffect_Controller : MonoBehaviour
    {
        private PageBook_Model _pageBook_Model;        
        [SerializeField] private Transform _playerBoardFirst;
        [SerializeField] private Transform _playerBoardSecond;

        /// <summary>
        /// список постоянных эффектов первого игрока
        /// </summary>
        public List<int> _activePermanentEffectPlayersFirst;

        /// <summary>
        /// список постоянных эффектов второго игрока
        /// </summary>
        public List<int> _activePermanentEffectPlayersSecond;

        private void OnEnable()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>(); 
        }

        public void AddPermanentEffect(Card_Controller card) //добавить эффект в список и применить его на поле
        {
            Card incomingCard = card.GetComponent<Card>();
            Card_Model cardModel = card.GetComponent<Card_Model>();

            if (incomingCard._side == Players.First)
                _activePermanentEffectPlayersFirst.Add(cardModel.IdCard);
            else
                _activePermanentEffectPlayersSecond.Add(cardModel.IdCard);

            AceptPermanentEffect(card);
        }

        public void RemovePermanentEffect(Card_Controller card) //отменить эффект с карт и удалить его из списка
        {
            Card incomingCard = card.GetComponent<Card>();
            Card_Model card_model = card.GetComponent<Card_Model>();            

            UpdatePermanentEffect(card_model, -card_model.ChangeHealthValue, -card_model.ChangeAtackValue);
            
            if (incomingCard._side == Players.First)
                _activePermanentEffectPlayersFirst.Remove(card_model.IdCard);
            else
                _activePermanentEffectPlayersSecond.Remove(card_model.IdCard);
        }
        private void AceptPermanentEffect(Card_Controller card) //применить эффект на картах которые стоят на поле
        {
            Card_Model card_model = card.GetComponent<Card_Model>();                

            UpdatePermanentEffect(card_model, card_model.ChangeHealthValue, card_model.ChangeAtackValue); //обновляем значения карт                                                                       
        }

        private void UpdatePermanentEffect(Card_Model cardModel, int changeHealthValue, int changeAtackValue)
        {
            Card incomingCard = cardModel.GetComponent<Card>();
            Card_Controller[] _cardPlayersFirst = _playerBoardFirst.GetComponentsInChildren<Card_Controller>();
            Card_Controller[] _cardPlayersSecond = _playerBoardSecond.GetComponentsInChildren<Card_Controller>();            

            if (incomingCard._side == Players.First)
                CheckBoard(_cardPlayersFirst, cardModel, changeHealthValue, changeAtackValue);
            else
                CheckBoard(_cardPlayersSecond, cardModel, changeHealthValue, changeAtackValue);
        }


        private void CheckBoard(Card_Controller[] _cardControllerArray, Card_Model cardModel, int changeHealthValue, int changeAtackValue)
        {
            for (int i = 0; i < _cardControllerArray.Length; i++)
            {
                Card_Model currentCard = _cardControllerArray[i].gameObject.GetComponent<Card_Model>();
                if (currentCard.IdCard == cardModel.IdCard) continue;

                if ((cardModel.IdCard == 102 || cardModel.IdCard == 107) && (cardModel._battleCryTargetsType == currentCard._minionType)) //баф карт 102 и 107
                {
                    _cardControllerArray[i].ChangeAtackValue(changeAtackValue);
                }

                if (cardModel.IdCard == 304)
                {
                    _cardControllerArray[i].ChangeAtackValue(changeAtackValue);
                }
            }

        }

        public void GetActivePermanentEffect(Card_Controller card) //берём уже действующий эффект на столе
        {
            Card incomingCard = card.GetComponent<Card>();

            if (incomingCard._side == Players.First)
                ApplyPermanentEffect(_activePermanentEffectPlayersFirst, card);
            else
                ApplyPermanentEffect(_activePermanentEffectPlayersSecond, card);
        }


        public void ApplyPermanentEffect(List<int> permanentEffect, Card_Controller card)
        {
            if (permanentEffect != null)
            {
                Card_Model card_Model = card.GetComponent<Card_Model>();
                foreach (int cardEffectId in permanentEffect)
                {
                    if (card_Model.IdCard == cardEffectId) continue;

                    CardSO_Model cardSO_Model = _pageBook_Model.GetCardSO_byID(cardEffectId);

                    if ((cardEffectId == 102 || cardEffectId == 107) && (cardSO_Model._targetsType == card_Model._minionType)) //баф карт 102 и 107
                    {
                        card.ChangeAtackValue(cardSO_Model.AbilityChangeAtack);
                    }

                    if (cardEffectId == 106)
                    {
                        //card.ChangeAtackValue(cardSO_Model._abilityChangeAtack);
                    }

                    if (cardEffectId == 304)
                    {
                        card.ChangeAtackValue(cardSO_Model.AbilityChangeAtack);
                    }

                    if (cardEffectId == 309 && cardSO_Model.AtackDamageCard <= 3)
                    {
                        card.GetComponent<Card>().EnableAttack();
                    }

                    if (cardEffectId == 505 && (cardSO_Model._targetsType == card_Model._minionType))
                    {
                        card.ChargeAbility();
                    }
                }
            }

        }
    }
}
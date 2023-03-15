using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class EventEffect_Controller : MonoBehaviour
    {
        private Mana_Controller _manaController;
        private PageBook_Model _pageBook_Model;
        //private int _lastEffect;
        [SerializeField] private Transform _playerFirstBoard;
        [SerializeField] private Transform _playerSecondBoard;

        /// <summary>
        /// коллекция карт на столе первого игрока
        /// </summary>
        private List<Card_Controller> _cardInBoardFirst = new List<Card_Controller>();
        /// <summary>
        /// коллекция карт на столе второго игрока
        /// </summary>
        private List<Card_Controller> _cardInBoardSecond = new List<Card_Controller>();
        /// <summary>
        /// список эффектов по событию у первого игрока
        /// </summary>
        private List<int> _activeEventEffectPlayerFirst = new List<int>();
        /// <summary>
        /// список эффектов по событию у второго игрока
        /// </summary>
        private List<int> _activeEventEffectPlayerSecond = new List<int>();

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

        public void AddEventEffect(Card_Controller card) //добавить эффект в список
        {
            Card incomingCard = card.GetComponent<Card>();
            Card_Model cardModel = card.GetComponent<Card_Model>();
            if (incomingCard._side == Players.First)
                _activeEventEffectPlayerFirst.Add(cardModel._idCard);
            else
                _activeEventEffectPlayerSecond.Add(cardModel._idCard);
        }

        public void RemoveEventEffect(Card_Controller card) //удалить эффект из списка
        {
            Card_Model card_model = card.GetComponent<Card_Model>();
            Card incomingCard = card.GetComponent<Card>();
            if (incomingCard._side == Players.First)
                _activeEventEffectPlayerFirst.Remove(card_model._idCard);
            else
                _activeEventEffectPlayerSecond.Remove(card_model._idCard);
        }

        /// <summary>
        /// обновляем списки карт на доске
        /// </summary>
        private void FillListMinion()
        {
            _cardInBoardFirst.Clear();
            _cardInBoardSecond.Clear();
            _cardInBoardFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>().ToList(); //все карты на столе первого игрока
            _cardInBoardSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>().ToList(); //все карты на столе второго игрока
        }

        /// <summary>
        /// проверка очерёдности хода
        /// </summary>
        /// <param name="card"></param>
        /// <param name="firstArrey"></param>
        /// <param name="secondArrey"></param>
        /// <returns></returns>
        private List<int> СheckTurnQueue(ApplyBattleCry card, List<int> firstArrey, List<int> secondArrey)
        {
            Card incomingCard = card.GetComponent<Card>();
            List<int> tempArrey;
            if (incomingCard._side == Players.First)
                tempArrey = firstArrey;
            else
                tempArrey = secondArrey;
            return tempArrey;
        }

        #region обработка события лечения
        /// <summary>
        /// обработка события лечения
        /// </summary>
        /// <param name="cardExample"></param>
        public void ParseHealEvent(ApplyBattleCry card)
        {
            List<int> tempArrey = СheckTurnQueue(card, _activeEventEffectPlayerFirst, _activeEventEffectPlayerSecond);
            if (tempArrey != null)
            {
                foreach (int cardEffectId in tempArrey)
                {
                    if (cardEffectId == 106)
                    {
                        card.gameObject.GetComponent<Card_Controller>().TakeAdditionalCard();
                    }
                }
            }
        }
        #endregion

        #region обработка события урона
        /// <summary>
        /// обработка события урона
        /// </summary>
        public void ParseDamageEvent(ApplyBattleCry card)
        {
            FillListMinion();
            List<int> tempArrey = СheckTurnQueue(card, _activeEventEffectPlayerFirst, _activeEventEffectPlayerSecond);

            if (tempArrey != null)
            {
                foreach (int cardEffectId in tempArrey) //обхожу весь список евент еффектов
                {
                    if (cardEffectId == 310) // если есть такой эффект
                    {
                        List<Card_Controller> cardsInBoard;
                        if (tempArrey == _activeEventEffectPlayerFirst)
                            cardsInBoard = _cardInBoardFirst;
                        else
                            cardsInBoard = _cardInBoardSecond;

                        foreach (Card_Controller cardController in cardsInBoard)
                        {
                            if (cardController.GetComponent<Card_Model>()._idCard == cardEffectId) //то вызываю метод Берсерк у карты с таким же Id
                            {
                                cardController.BerserkAbility();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region обработка события смерти
        /// <summary>
        /// обработка события смерти
        /// </summary>
        public void ParseDeathEvent(Card_Controller cardExample)
        {
            FillListMinion();
            MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //тип миньона вызвавшего событие
            ApplyBattleCry card = cardExample.GetComponent<ApplyBattleCry>();
            List<int> tempArrey = СheckTurnQueue(card, _activeEventEffectPlayerFirst, _activeEventEffectPlayerSecond);

            if (tempArrey != null)
            {
                foreach (int cardEffectId in tempArrey) //обхожу весь список евент еффектов
                {
                    if (cardEffectId == 212) // если есть такой эффект
                    {
                        List<Card_Controller> cardsInBoard;
                        if (tempArrey == _activeEventEffectPlayerFirst)
                            cardsInBoard = _cardInBoardFirst;
                        else
                            cardsInBoard = _cardInBoardSecond;
                        foreach (Card_Controller cardController in cardsInBoard)
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
        #endregion

        #region обработка события установки карты на доску
        /// <summary>
        /// обработка события усатновки карты на стол
        /// </summary>
        /// <param name="cardExample"></param>
        public void ParsePutCardInBoard(Card_Controller cardExample)
        {
            FillListMinion();
            MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //тип миньона вызвавшего событие
            ApplyBattleCry card = cardExample.GetComponent<ApplyBattleCry>();
            List<int> tempArrey = СheckTurnQueue(card, _activeEventEffectPlayerFirst, _activeEventEffectPlayerSecond);
            if (tempArrey != null)
            {
                foreach (int cardEffectId in tempArrey) //обхожу весь список евент еффектов
                {
                    if (cardEffectId == 213) // если есть такой эффект
                    {
                        List<Card_Controller> cardsInBoard;
                        if (tempArrey == _activeEventEffectPlayerFirst)
                            cardsInBoard = _cardInBoardFirst;
                        else
                            cardsInBoard = _cardInBoardSecond;
                        foreach (Card_Controller cardController in cardsInBoard)
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
                        List<Card_Controller> cardsInBoard;
                        if (tempArrey == _activeEventEffectPlayerFirst)
                            cardsInBoard = _cardInBoardFirst;
                        else
                            cardsInBoard = _cardInBoardSecond;
                        foreach (Card_Controller cardController in cardsInBoard)
                        {
                            cardController.ChangeHealtValue(card_Model._abilityChangeHealth, ChangeHealthType.Healing);
                        }
                        RemoveEventEffect(cardExample);
                    }

                }
                /*
                 ставим новую карту
                вызвать событие - если активен эффект 213 и это зверь, то взять карту
                 */
            }
        }
        #endregion

        #region обработка события смены хода
        /// <summary>
        /// обработка события смены хода
        /// </summary>
        /// <param name="playersTurn"></param>
        public void ParseChangeTurnEvent(Players playersTurn)
        {
            FillListMinion();
            List<Card_Controller> cardsInBoard;
            if (playersTurn == Players.First)
                cardsInBoard = _cardInBoardFirst;
            else
                cardsInBoard = _cardInBoardSecond;
            if (cardsInBoard == null) return;

            foreach (Card_Controller cardModel1 in cardsInBoard)
            {
                Card_Model card_Model1 = cardModel1.GetComponent<Card_Model>();
                if (card_Model1._idCard == 211/* && ((playersTurn == Players.First && card_Model1.transform.parent == _playerFirstBoard) || (playersTurn == Players.Second && card_Model1.transform.parent == _playerSecondBoard))*/)
                {
                    List<Card_Model> tempListCardModel = new List<Card_Model>(); //временный лист раненых
                    foreach (Card_Controller cardModel2 in cardsInBoard)
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
        #endregion
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Hearthstone
{
    public class Card_Controller : MonoBehaviour
    {
        private HandManager _handManager;
        private Card_Model _card_Model;
        private BattleCry_Controller _battleCryController;
        private PermanentEffect_Controller _permanentEffectController;
        private SingleEffect_Controller _singleEffect_Controller;
        private EventEffect_Controller _eventEffectController;
        private SoundEffect_Controller _soundEffect_Controller;
        private BattleModeCard_View _battleModeCardView;
        private IndicatorTarget _indicatorTarget;
        private Action OnActivateCard;
        private Board _board;
        //[Inject]
        private MulliganManager _mulliganManager;
        //[Inject]
        private Mana_Controller _mana_Controller;
        private bool _useBattleCray = false;
        public bool UseBattleCray { get => _useBattleCray; set => _useBattleCray = value; }

        private void OnEnable()
        {
            //пока костыль. заменить на Zenject
            _mana_Controller = FindObjectOfType<Mana_Controller>();
            _mulliganManager = FindObjectOfType<MulliganManager>();            
            _battleCryController = FindObjectOfType<BattleCry_Controller>();            
            _permanentEffectController = FindObjectOfType<PermanentEffect_Controller>();
            _eventEffectController = FindObjectOfType<EventEffect_Controller>();
            _soundEffect_Controller = FindObjectOfType<SoundEffect_Controller>();
            _singleEffect_Controller = FindObjectOfType<SingleEffect_Controller>();
            _handManager = FindObjectOfType<HandManager>();
            _battleModeCardView = GetComponent<BattleModeCard_View>();
            _card_Model = GetComponent<Card_Model>();
            _indicatorTarget = FindObjectOfType<IndicatorTarget>();

            OnActivateCard += ChoiseAbility;
        }
        private void OnDisable()
        {
            _board.EndDragCard -= ActivateBattleCry;
            OnActivateCard -= ChoiseAbility;
        }

        private void Start()
        {
            Players side = GetComponent<BattleModeCard>().GetSide();
            _board = FindObjectsOfType<Board>().Where(board => board._side == side).FirstOrDefault();
            _board.EndDragCard += ActivateBattleCry;
        }

        public void ActivateBattleCry(Card card) //активаци€ боевых кличей
        {
            _soundEffect_Controller.PlaySound(_soundEffect_Controller.PutCardInBoard);

            //_battleCryController.IsActiveCry = true;
            Card_Model card_Model = card.GetComponent<Card_Model>();
            if (card_Model._battleCryTargets == Target.Single || card_Model._battleCryTargets == Target.SingleFriend || card_Model._battleCryTargets == Target.Hero)
                _battleCryController.IsActiveCry = true;

            var card_model = card.GetComponent<Card_Model>().NameCard;            
            Card my_Card = GetComponent<Card>();

            if (card != my_Card) return;
            {
                //Debug.Log(card_model);
                _battleModeCardView.ChangeCardViewMode();
                SaveValueCurrentBattleCry();
                _battleCryController.UpdateBattleCry();
                OnActivateCard?.Invoke();

                if (_card_Model._battleCryTypes.Contains(BattleCryType.SummonAssistant))//призыв существа-ассистента
                {
                    int minionID = int.Parse(_card_Model.IdCard.ToString() + _card_Model.IdCard.ToString());                    
                    Transform transform = new GameObject().transform;//исправить
                    transform.position = _board.GetLastCardPosition();
                    int layout = 0;
                    _handManager.CreateCard(_board._side, transform, ref layout, minionID, true);

                    Destroy(transform.gameObject);
                }

                if (_card_Model._battleCryTypes.Contains(BattleCryType.PermanentEffect))
                {
                    _permanentEffectController.AddPermanentEffect(this);

                }
                _permanentEffectController.GetActivePermanentEffect(this);//?

                if (_card_Model._battleCryTypes.Contains(BattleCryType.EventEffect))
                {
                    _eventEffectController.AddEventEffect(this);

                }
                _eventEffectController.ParsePutCardInBoard(this);

                if (_card_Model._battleCryTypes.Contains(BattleCryType.SingleEffect))                
                {
                    _singleEffect_Controller.ApplyEffect(gameObject.GetComponent<ApplyBattleCry>());
                }

            }
        }

        //сохран€ем значение боевого клича
        private void SaveValueCurrentBattleCry()
        {
            _useBattleCray = true;

            _battleCryController.BattleCryCreator = _card_Model.gameObject;

            _battleCryController.IdBattleCry = _card_Model.IdCard;
            _battleCryController._battleCryTargets_Active = _card_Model._battleCryTargets;
            _battleCryController._battleCryTargetsType_Active = _card_Model._battleCryTargetsType;
            _battleCryController.BattleCryChangeAtackDamage = _card_Model.ChangeAtackValue;
            _battleCryController.BattleCryChangeHealth = _card_Model.ChangeHealthValue;

            _battleCryController._currentBattleCryTypes.Clear();
            foreach (BattleCryType cryType in _card_Model._battleCryTypes)
            {
                _battleCryController._currentBattleCryTypes.Add(cryType);
            }
            _battleCryController._curentAbilityInTarget.Clear();
            foreach (AbilityCurrentCard abilityInTarget in _card_Model._abilityInTargetBattleCry)
            {
                _battleCryController._curentAbilityInTarget.Add(abilityInTarget);
            }

            //if (_card_Model._battleCryTargets != Target.Single || _card_Model._battleCryTargets != Target.SingleFriend)
            //    _battleCryController.IsActiveCry = false;

        }

        #region //Ability —ondition

        /// <summary>
        /// выбор активной способности
        /// </summary>
        public void ChoiseAbility() //выбор активной способности
        {
            foreach (var isActive in _card_Model._activeAbility)
            {
                if (isActive.Value == true)
                {
                    ActivateAbility(isActive.Key);
                }
            }
        }

        /// <summary>
        /// включение активной способности
        /// </summary>
        /// <param name="currentActiveAbility"></param>
        private void ActivateAbility(AbilityCurrentCard currentActiveAbility) //активаци€ активной способности
        {
            switch (currentActiveAbility)
            {
                //case AbilityCurrentCard.PermanentEffect:
                //    //PermanentEffectAbility();
                //    break;
                case AbilityCurrentCard.Provocation:
                    ProvocationAbility(_card_Model._isProvocation);
                    break;
                //case AbilityCurrentCard.Charge:
                //    ChargeAbility();
                //    break;
                case AbilityCurrentCard.GetCard:
                    TakeAdditionalCard(GetComponent<Card>().GetSide());
                    break;
            }
        }
        /*private void PermanentEffectAbility()//посто€нный эффект
        {

        }*/
        public void ProvocationAbility(bool isProvocation) //провокаци€
        {
            if (isProvocation)
            {
                _card_Model.ProtectionImage.gameObject.SetActive(isProvocation);                
                _soundEffect_Controller.PlaySound(_soundEffect_Controller.Taunt);
            }
            else
                _card_Model.ProtectionImage.gameObject.SetActive(!isProvocation);
        }
        //public void ChargeAbility()//рывок
        //{
        //    Debug.Log($"{_card_Model.NameCard} может атаковать на этом ходу");
        //    //_card_Model._isCharge = true;
        //}
        public void TakeAdditionalCard(Players side) //добавление новой карты
        {
            StartCoroutine(Pause(1, side));                        
        }
        IEnumerator Pause(float pauseValue, Players side)
        {
            yield return new WaitForSeconds(pauseValue);
            _soundEffect_Controller.PlaySound(_soundEffect_Controller.DrawCard);
            _mulliganManager.TakeOneCard(side);            
        }

        #endregion

        #region //Ability Action

        public void ChangeAtackValue(int incomingValue) //измен€ем значение атаки
        {
            _card_Model.AtackDamageCard += incomingValue;
            _card_Model.MaxAtackValue += incomingValue;
            _battleModeCardView.UpdateViewCard();

        }

        public void ChangeHealthValue(int incomingValue) //измен€ем значение здоровь€
        {
            _card_Model.HealthCard += incomingValue;
            if (_card_Model.HealthCard <= 0)
                DiedCreature(); //событие смерти
            if (incomingValue < 0 && _card_Model._isBerserk) //если карта берсерк и здоровье уменьшилось, то увеличиваем атаку 
                ChangeAtackValue(_card_Model.ChangeAtackValue);
            _battleModeCardView.UpdateViewCard();
        }

        public void ChangeHealthValue(int incomingValue, ChangeHealthType changeHealthType) //алтернативный вариант //////////////////////////////////////////
        {
            if (changeHealthType == ChangeHealthType.DealDamage)
            {
                ChangeHealthValue(-incomingValue);
                _singleEffect_Controller.ApplyEffect(gameObject.GetComponent<ApplyBattleCry>());                
            }

            if (changeHealthType == ChangeHealthType.Healing)
            {
                ChangeHealthValue(incomingValue);
                if (_card_Model.HealthCard > _card_Model.MaxHealtValue)
                    _card_Model.HealthCard = _card_Model.MaxHealtValue;
                _battleModeCardView.UpdateViewCard();
                _singleEffect_Controller.ApplyEffect(gameObject.GetComponent<ApplyBattleCry>());

                StartCoroutine(_battleModeCardView.EffectParticle(_battleModeCardView.HealtEffect));
                //_battleModeCardView.UpdateViewCard();
            }

            if (changeHealthType == ChangeHealthType.RaiseParametrs)
            {
                ChangeHealthValue(incomingValue);
                //_singleEffect_Controller.ApplyEffect(gameObject.GetComponent<ApplyBattleCry>());
            }
        }

        public void UpdateSelfParametrs(int multiplicationFactor) //увеличение своих параметров в зависимости от колличества дружеских карт на столе
        {
            ChangeAtackValue(_card_Model.ChangeAtackValue * multiplicationFactor);
            ChangeHealthValue(_card_Model.ChangeHealthValue * multiplicationFactor);
        }

        public void BerserkAbility()
        {
            ChangeAtackValue(_card_Model.ChangeAtackValue);
        }

        public void DiedCreature() //смерть существа
        {           

            _permanentEffectController.RemovePermanentEffect(this);
            _eventEffectController.RemoveEventEffect(this);
            _eventEffectController.ParseDeathEvent(this);

            _indicatorTarget.ChangeCursorState(false);

            gameObject.SetActive(false);

            Card card = GetComponent<Card>();
            Board parent = transform.parent.GetComponent<Board>();
            parent.RemoveCard(card);
            parent.ReorderMinions();
        }

        #endregion        
    }
}
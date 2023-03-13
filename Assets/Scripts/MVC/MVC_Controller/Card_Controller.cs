using System;
using System.Collections.Generic;
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
        private BattleModeCard_View _battleModeCardView;
        public bool _useBattleCray = false;

        private Action OnActivateCard;


        private Board _board;
        //[Inject]
        private MulliganManager _mulliganManager;
        //[Inject]
        private Mana_Controller _mana_Controller;

        private void OnEnable()
        {
            //���� �������. �������� �� Zenject
            _mana_Controller = FindObjectOfType<Mana_Controller>();
            _mulliganManager = FindObjectOfType<MulliganManager>();
            
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            //_battleModeCardView = FindObjectOfType<BattleModeCard_View>();
            
            _permanentEffectController = FindObjectOfType<PermanentEffect_Controller>();
            _eventEffectController = FindObjectOfType<EventEffect_Controller>();
            _singleEffect_Controller = FindObjectOfType<SingleEffect_Controller>();
            _handManager = FindObjectOfType<HandManager>();
            _battleModeCardView = GetComponent<BattleModeCard_View>();
            _card_Model = GetComponent<Card_Model>();

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

        public void ActivateBattleCry(/*Transform newParent*/Card card) //��������� ������ ������
        {
            _battleCryController._isActiveCry = true;
            //        if (newParent == transform.parent && !_useBattleCray)
            //         if (card.transform.parent == _board.transform && !_useBattleCray)
            var card_model = card.GetComponent<Card_Model>()._nameCard;
            

            var my_Card = GetComponent<Card>();

            if (card != my_Card) return;
            {
                //Debug.Log(card_model);
                _battleModeCardView.ChangeCardViewMode();
                SaveValueCurrentBattleCry();
                _battleCryController.UpdateBattleCry();
                OnActivateCard?.Invoke();

                if (_card_Model._battleCryTypes.Contains(BattleCryType.SummonAssistant))//������ ��������-����������
                {
                    int minionID = int.Parse(_card_Model._idCard.ToString() + _card_Model._idCard.ToString());
                    //Board board = newParent.GetComponent<Board>();
                    
                    Transform transform = new GameObject().transform;//���������
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
                //if(_card_Model._battleCryTargets == Target.Self)
                {
                    _singleEffect_Controller.ApplyEffect(gameObject.GetComponent<ApplyBattleCry>());
                }

            }
        }

        //��������� �������� ������� �����
        private void SaveValueCurrentBattleCry()
        {
            _useBattleCray = true;

            _battleCryController._battleCryCreator = _card_Model.gameObject;

            _battleCryController._idBattleCry = _card_Model._idCard;
            _battleCryController._battleCryTargets_Active = _card_Model._battleCryTargets;
            _battleCryController._battleCryTargetsType_Active = _card_Model._battleCryTargetsType;
            _battleCryController._battleCryChangeAtackDamage = _card_Model._changeAtackValue;
            _battleCryController._battleCryChangeHealth = _card_Model._�hangeHealthValue;

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
        }

        #region //Ability �ondition

        /// <summary>
        /// ����� �������� �����������
        /// </summary>
        public void ChoiseAbility() //����� �������� �����������
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
        /// ��������� �������� �����������
        /// </summary>
        /// <param name="currentActiveAbility"></param>
        private void ActivateAbility(AbilityCurrentCard currentActiveAbility) //��������� �������� �����������
        {
            switch (currentActiveAbility)
            {
                case AbilityCurrentCard.PermanentEffect:
                    PermanentEffectAbility();
                    break;
                case AbilityCurrentCard.Provocation:
                    ProvocationAbility(_card_Model._isProvocation);
                    break;
                case AbilityCurrentCard.Charge:
                    ChargeAbility();
                    break;
                case AbilityCurrentCard.GetCard:
                    TakeAdditionalCard();
                    break;
            }
        }
        private void PermanentEffectAbility()//���������� ������
        {

        }
        public void ProvocationAbility(bool isProvocation) //����������
        {
            if (isProvocation)
            {
                _card_Model._protectionImage.gameObject.SetActive(isProvocation);
            }
            if (!isProvocation)
            {
                _card_Model._protectionImage.gameObject.SetActive(!isProvocation);
            }
        }
        public void ChargeAbility()//�����
        {
            Debug.Log($"{_card_Model._nameCard} ����� ��������� �� ���� ����");
            //_card_Model._isCharge = true;
        }
        public void TakeAdditionalCard() //���������� ����� �����
        {
            _mulliganManager.TakeOneCard(_mana_Controller.WhoMovesNow());
        }

        #endregion

        #region //Ability Action

        public void ChangeAtackValue(int incomingValue) //�������� �������� �����
        {
            _card_Model._atackDamageCard += incomingValue;
            _card_Model._maxAtackValue += incomingValue;
            _battleModeCardView.UpdateViewCard();

        }

        public void ChangeHealtValue(int incomingValue) //�������� �������� ��������
        {
            _card_Model._healthCard += incomingValue;
            if (incomingValue < 0 && _card_Model._isBerserk) //���� ����� ������� � �������� �����������, �� ����������� ����� 
                ChangeAtackValue(_card_Model._changeAtackValue);
            _battleModeCardView.UpdateViewCard();
        }

        public void ChangeHealtValue(int incomingValue, ChangeHealthType changeHealthType) //������������� ������� //////////////////////////////////////////
        {
            if (changeHealthType == ChangeHealthType.DealDamage)
            {
                ChangeHealtValue(-incomingValue);
                _singleEffect_Controller.ApplyEffect(gameObject.GetComponent<ApplyBattleCry>());
                if (_card_Model._healthCard <= 0)
                    DiedCreature(); //������� ������
            }

            if (changeHealthType == ChangeHealthType.Healing)
            {
                ChangeHealtValue(incomingValue);
                if (_card_Model._healthCard > _card_Model._maxHealtValue)
                    _card_Model._healthCard = _card_Model._maxHealtValue;
                _battleModeCardView.UpdateViewCard();
                _singleEffect_Controller.ApplyEffect(gameObject.GetComponent<ApplyBattleCry>());
                StartCoroutine(_battleModeCardView.EffectParticle(_battleModeCardView._healtEffect));
                //_battleModeCardView.UpdateViewCard();
            }
        }

        public void UpdateSelfParametrs(int multiplicationFactor) //���������� ����� ���������� � ����������� �� ����������� ��������� ���� �� �����
        {
            ChangeAtackValue(_card_Model._changeAtackValue * multiplicationFactor);
            ChangeHealtValue(_card_Model._�hangeHealthValue * multiplicationFactor);
        }

        public void BerserkAbility()
        {
            ChangeAtackValue(_card_Model._changeAtackValue);
        }

        public void DiedCreature() //������ ��������
        {
            Debug.Log(_card_Model._nameCard + " ����� ������� �������");


            _permanentEffectController.RemovePermanentEffect(this);
            _eventEffectController.RemoveEventEffect(this);
            _eventEffectController.ParseDeathEvent(this);

            gameObject.SetActive(false);
        }

        #endregion        
    }
}
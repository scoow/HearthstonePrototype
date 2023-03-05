using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Hearthstone
{
    public class Card_Controller : MonoBehaviour
    {
        private Card_Model _card_Model;
        private BattleCry_Controller _battleCryController;
        private PermanentEffect_Controller _permanentEffectController;
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
            _card_Model = GetComponent<Card_Model>();
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _battleModeCardView = FindObjectOfType<BattleModeCard_View>();
            _permanentEffectController = FindObjectOfType<PermanentEffect_Controller>();


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

        public void ActivateBattleCry(Transform newParent) //��������� ������ ������
        {
            _battleCryController._isActiveCry = true;
            if (newParent == transform.parent && !_useBattleCray)
            {
                _battleModeCardView.ChangeCardViewMode();               
                SaveValueCurrentBattleCry();                
                _battleCryController.UpdateBattleCry();
                OnActivateCard?.Invoke();

                if(_card_Model._battleCryTypes.Contains(BattleCryType.PermanentEffect))
                _permanentEffectController.AddEffect(_card_Model._idCard);
                _permanentEffectController.GetActiveEffectInCard(this);               
            }  
        }

        //��������� �������� ������� �����
        private void SaveValueCurrentBattleCry()
        {
            _useBattleCray = true;
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


        private void ApplyAbilityInSelf(Transform transformParent, BattleCryType cryType) //��������� ������ ���� �� ���� 
        {
            ApplyBattleCry[] _temporaryArray = transformParent.GetComponentsInChildren<ApplyBattleCry>();
            if (_card_Model._battleCryTargets == Target.Self)
            {
                _card_Model.GetComponent<Card_Controller>().UpdateSelfParametrs(_temporaryArray.Length - 1);
                StartCoroutine(_battleModeCardView.EffectParticle(_battleModeCardView._scaleEffect));
            }
            if (cryType == BattleCryType.GetCardInDeck)
                TakeAdditionalCard();
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
                case AbilityCurrentCard.Berserk:

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
            if(!isProvocation)
            {
                _card_Model._protectionImage.gameObject.SetActive(!isProvocation);
            }
        }
        public void ChargeAbility()//�����
        {
            Debug.Log($"{_card_Model._nameCard} ����� ��������� �� ���� ����");
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

        public void UpdateSelfParametrs(int multiplicationFactor) //���������� ����� ���������� � ����������� �� ����������� ��������� ���� �� �����
        {
            ChangeAtackValue(_card_Model._changeAtackValue* multiplicationFactor);
            ChangeHealtValue(_card_Model._�hangeHealthValue* multiplicationFactor);            
        }

        public void DiedCreature() //������ ��������
        {            
            gameObject.SetActive(false);
            _permanentEffectController.RemoveEffect(_card_Model._idCard);
        }

        #endregion        
    }    
}
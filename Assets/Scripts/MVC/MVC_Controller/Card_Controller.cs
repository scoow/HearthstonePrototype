using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Hearthstone
{
    public class Card_Controller : MonoBehaviour
    {
        private Card_Model _card_Model;
        private BattleCry_Controller _battleCryController;
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
            OnActivateCard += ChoiseAbility;

        }
        private void OnDisable()
        {            
            _board.EndDragCard -= ActivateBattleCry;
            OnActivateCard -= ChoiseAbility;
        }

        private void Start()
        {
            //ProvocationAbility();

            Players side = GetComponent<BattleModeCard>().GetSide();
            _board = FindObjectsOfType<Board>().Where(board => board._side == side).FirstOrDefault();
            _board.EndDragCard += ActivateBattleCry;
        }

        public void ActivateBattleCry(Transform newParent) //��������� ������ ������
        {
            if (newParent == transform.parent && !_useBattleCray)
            {
                _battleModeCardView.ChangeCardViewMode();                
                _useBattleCray = true;
                if (_card_Model._battleCryType != BattleCryType.No�ry && _card_Model._battleCryType != BattleCryType.UseAbility)
                {
                    _battleCryController._idBattleCry = _card_Model._idCard;
                    _battleCryController._battleCryType = _card_Model._battleCryType;
                    _battleCryController._battleCryTargets = _card_Model._battleCryTargets;
                    _battleCryController._battleCryTargetsType = _card_Model._battleCryTargetsType;                    
                    _battleCryController._isActiveCry = true;
                    _battleCryController.UpdateBattleCry();

                    ApplyAbilityInSelf(newParent);
                }                
                OnActivateCard?.Invoke();
            }

            
            
            //ApplyAbilityInSelf(newParent);
        }


        private void ApplyAbilityInSelf(Transform transformParent) //��������� ������ ���� �� ���� 
        {
            ApplyBattleCry[] _temporaryArray = transformParent.GetComponentsInChildren<ApplyBattleCry>();
            if (_card_Model._battleCryTargets == BattleCryTargets.Self)
            {
                _card_Model.GetComponent<Card_Controller>().UpdateSelfParametrs(_temporaryArray.Length - 1);
                StartCoroutine(_battleModeCardView.EffectParticle(_battleModeCardView._scaleEffect));
            }
            if (_card_Model._battleCryType == BattleCryType.GetCardInDeck)
                TakeAdditionalCard();
        }


        public void DiedCreature()
        {
            Debug.Log("�������� �������");
            gameObject.SetActive(false);
        }

        #region //Ability �ondition

        private void ChoiseAbility() //����� �������� �����������
        {            
            foreach (var isActive in _card_Model._activeAbility)
            {               
                if (isActive.Value == true)
                {
                    ActivateAbility(isActive.Key);
                }
            }
        }

        private void ActivateAbility(string currentNameActiveAbility) //��������� �������� �����������
        {            
            
            Debug.Log("��������� �������� �����������");
            switch (currentNameActiveAbility)
            {
                case "PermanentEffect":
                    PermanentEffectAbility();
                    break;
                case "DivineShield":
                    DivineShieldAbility();
                    break;
                case "Provocation":
                    ProvocationAbility();
                    break;
                case "Charge":
                    ChargeAbility();
                    break;
                case "GetCard":
                    TakeAdditionalCard();
                    break;
            }
            
        }

        private void ProvocationAbility() //����������
        {
            if (_card_Model._isProvocation == true)
            {
                _card_Model._protectionImage.gameObject.SetActive(true);
            }
        }

        private void DivineShieldAbility() //������������ ���
        {
            if (_card_Model._isDivineShield == true)
            {
                Debug.Log("����� ������������ ������ ����");
            }
        }

        private void PermanentEffectAbility()//���������� ������
        {

        }

        private void ChargeAbility()//�����
        {

        }
        public void TakeAdditionalCard() //���������� ����� �����
        {
            Debug.Log("���������� ����� �����");
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
            if (_battleCryController._battleCryType == BattleCryType.Heal) //�������
            {
                _card_Model._healthCard += incomingValue;
                if (_card_Model._healthCard > _card_Model._maxHealtValue)
                    _card_Model._healthCard = _card_Model._maxHealtValue;
                _battleModeCardView.UpdateViewCard();
            }
            else if (_battleCryController._battleCryType == BattleCryType.DealDamage)//��������� ����
            {
                _card_Model._healthCard -= incomingValue;
                _battleModeCardView.UpdateViewCard();
                if (_card_Model._healthCard <= 0) DiedCreature(); //������� ������
            }
            else if (_battleCryController._battleCryType == BattleCryType.RaiseParametrs)//�������� ���������
            {       
                _card_Model._healthCard += incomingValue;
                    if (incomingValue > 0)
                _card_Model._maxHealtValue += incomingValue;
                _battleModeCardView.UpdateViewCard();                
            }
        }

        public void UpdateSelfParametrs(int multiplicationFactor) //���������� ����� ���������� � ����������� �� ����������� ��������� ���� �� �����
        {
            ChangeAtackValue(_card_Model._abilityChangeAtackDamage* multiplicationFactor);
            ChangeHealtValue(_card_Model._abilityChangeHealth * multiplicationFactor);            
        }

        public void BerserkAbility() //������ ��������
        {
            ChangeAtackValue(_card_Model._abilityChangeAtackDamage);
        }        

        #endregion        
    }    
}
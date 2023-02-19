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
        private Board _board;
        [Inject]
        private MulliganManager _mulliganManager;

        private void OnEnable()
        {
            _board = FindObjectOfType<Board>();
            _card_Model = GetComponent<Card_Model>();
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _battleModeCardView = FindObjectOfType<BattleModeCard_View>();

            _board.EndDragCard += ActivateBattleCry;

        }
        private void OnDisable()
        {            
            _board.EndDragCard -= ActivateBattleCry;
        }

        private void Start()
        {
            ProvocationAbility();
        }

        public void ActivateBattleCry(Transform newParent) //��������� ������ ������
        {
            if (newParent == transform.parent && !_useBattleCray)
            {
                _battleModeCardView.ChangeCardViewMode();                
                _useBattleCray = true;
                if (_card_Model._battleCryType != BattleCryType.No�ry)
                {
                    _battleCryController._idBattleCry = _card_Model._idCard;
                    _battleCryController._battleCryType = _card_Model._battleCryType;
                    _battleCryController._battleCryTargets = _card_Model._battleCryTargets;
                    _battleCryController._battleCryTargetsType = _card_Model._battleCryTargetsType;                    
                    _battleCryController._isActiveCry = true;
                    _battleCryController.UpdateBattleCry();

                    //��������� ������ ���� �� ���� 
                    ApplyBattleCry[] _temporaryArray = newParent.GetComponentsInChildren<ApplyBattleCry>();
                    if (_card_Model._battleCryTargets == BattleCryTargets.Self)
                        _card_Model.GetComponent<Card_Controller>().UpdateSelfParametrs(_temporaryArray.Length - 1);  
                }                
            }                      
        }


        public void DiedCreature()
        {
            Debug.Log("�������� �������");
        }

        #region //Ability �ondition
        private void ProvocationAbility()
        {
            if (_card_Model._isProvocation == true)
            {
                _card_Model._protectionImage.gameObject.SetActive(true);
            }
        }

        private void DivineShieldAbility()
        {
            if (_card_Model._isDivineShield == true)
            {
                Debug.Log("����� ������������ ������ ����");
            }
        }

        private void PermanentEffectAbility()
        {

        }

        private void ChargeAbility()
        {

        }
        
        #endregion

        #region //Ability Action
               
        

        public void ChangeAtackValue(int incomingValue)
        {
            _card_Model._atackDamageCard += incomingValue;
            _card_Model._maxAtackValue = _card_Model._atackDamageCard;
            _battleModeCardView.UpdateViewCard();
        }

        public void ChangeHealtValue(int incomingValue) //�������� �������� ��������
        {
            if(_battleCryController._battleCryType == BattleCryType.Heal) //�������
            {
                _card_Model._healthCard += incomingValue;
                if (_card_Model._healthCard > _card_Model._maxHealtValue)
                    _card_Model._healthCard = _card_Model._maxHealtValue;
                _battleModeCardView.UpdateViewCard();
            }
            else if(_battleCryController._battleCryType == BattleCryType.DealDamage)//��������� ����
            {
                _card_Model._healthCard -= incomingValue;
                _battleModeCardView.UpdateViewCard();
                if (_card_Model._healthCard <= 0) DiedCreature(); //������� ������
            }
            else if(_battleCryController._battleCryType == BattleCryType.RaiseParametrs)//����������� ���������
            {
                _card_Model._healthCard += incomingValue;
                _card_Model._maxHealtValue = _card_Model._healthCard;
                _battleModeCardView.UpdateViewCard();
            }            
        }

        public void UpdateSelfParametrs(int multiplicationFactor) //���������� ����� ���������� � ����������� �� ����������� ��������� ���� �� �����
        {
            ChangeAtackValue(_card_Model._abilityChangeAtackDamage* multiplicationFactor);
            ChangeHealtValue(_card_Model._abilityChangeHealth * multiplicationFactor);            
        }

        public void BerserkAbility()
        {
            ChangeAtackValue(_card_Model._abilityChangeAtackDamage);
        }
        public void TakeAdditionalCard()
        {
            _mulliganManager.TakeOneCard();
        }

        #endregion
    }
}
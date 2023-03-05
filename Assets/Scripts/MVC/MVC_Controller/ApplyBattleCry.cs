using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Hearthstone
{
    public class ApplyBattleCry : MonoBehaviour, IPointerClickHandler
    {
        private BattleCry_Controller _battleCryController;
        private BattleModeCard_View _battleModeCard_View;
        private EventEffect_Controller _eventEffectController;
        private Card_Model _card_Model;
        private Card_Controller _card_Controller;
        [Inject]
        private PageBook_Model _pageBook_Model;
        public bool _isListen = true; //����� �� ������� ����� ������ ����

        private void OnEnable()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>(); //FindObjectOfType<PageBook_Model>();
            _eventEffectController = FindObjectOfType<EventEffect_Controller>();
            _card_Model = GetComponent<Card_Model>();
            _card_Controller = GetComponent<Card_Controller>();
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _battleModeCard_View = GetComponent<BattleModeCard_View>();          
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (transform.parent.gameObject.GetComponent<Board>()
                && _isListen && _battleCryController._isActiveCry
                && (_battleCryController._battleCryTargets_Active != Target.Self))//���������� ���� ������� �����
            {   
                if(_battleCryController._idBattleCry != _card_Model._idCard) //��������� ���������� ������� ����� �� ����
                {                    
                    ApplyNewBattleCry();
                    _isListen = false;
                    _battleCryController._isActiveCry = false;
                    //_battleCryController._isActiveCry = true;
                    _battleCryController.UpdateBattleCry();
                    
                }                
            }
        }

        //��������� ������ ����
        public void ApplyNewBattleCry()
        {

            foreach (AbilityCurrentCard abilityInTarget in _battleCryController._curentAbilityInTarget)
            {
                if((_battleCryController._battleCryTargetsType_Active == _card_Model._minionType) || _battleCryController._battleCryTargetsType_Active == MinionType.None )
                {      
                    if(abilityInTarget == AbilityCurrentCard.Provocation)
                    {
                        _card_Controller.ProvocationAbility(true);
                    }


                    //�������� �����
                    if (abilityInTarget == AbilityCurrentCard.ChangeAtack) 
                    {
                        _card_Controller.ChangeAtackValue(_battleCryController._battleCryChangeAtackDamage);
                    }
                    //�������� ��������
                    if (abilityInTarget == AbilityCurrentCard.ChangeHealt) 
                    {
                        foreach(BattleCryType cryType in _battleCryController._currentBattleCryTypes)
                        {
                            if (cryType == BattleCryType.DealDamage) //��������� ����
                            {
                                _eventEffectController.ParseDamageEvent();
                                _card_Controller.ChangeHealtValue(-_battleCryController._battleCryChangeHealth);                               
                                if (_card_Model._healthCard <= 0)
                                    _card_Controller.DiedCreature(); //������� ������
                            }                                

                            if (cryType == BattleCryType.Heal)//����� ����
                            {
                                if (_card_Model._healthCard < _card_Model._maxHealtValue)
                                    _eventEffectController.ParseHealEvent(this);
                                _card_Controller.ChangeHealtValue(_battleCryController._battleCryChangeHealth);
                                if (_card_Model._healthCard > _card_Model._maxHealtValue)
                                    _card_Model._healthCard = _card_Model._maxHealtValue;
                                _battleModeCard_View.UpdateViewCard();
                                StartCoroutine(_battleModeCard_View.EffectParticle(_battleModeCard_View._healtEffect));
                            }
                            if(cryType == BattleCryType.RaiseParametrs) //�������� ���������
                            {
                                _card_Model._maxHealtValue += _battleCryController._battleCryChangeHealth;
                                _card_Controller.ChangeHealtValue(_battleCryController._battleCryChangeHealth);                                
                                _battleModeCard_View.UpdateViewCard();
                            }
                        } 
                    }
                }               
            }
        }      
    }
}
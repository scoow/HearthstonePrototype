using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
using Zenject;

namespace Hearthstone
{
    public class ApplyBattleCry : MonoBehaviour, IPointerClickHandler
    {
        private BattleCry_Controller _battleCryController;
        private BattleModeCard_View _battleModeCard_View;
        private EventEffect_Controller _eventEffectController;
        private SingleEffect_Controller _singleEffectController;
        private Card_Model _card_Model;
        private Card_Controller _card_Controller;
        [Inject]
        private PageBook_Model _pageBook_Model;

        /// <summary>
        /// ����� �� ���� ������� ����� ������ ����
        /// </summary>
        [SerializeField] private bool _isListen = true;
        public bool IsListen { get => _isListen; set => _isListen = value; }

        private void OnEnable()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>(); //FindObjectOfType<PageBook_Model>();
            _eventEffectController = FindObjectOfType<EventEffect_Controller>();
            _singleEffectController = FindObjectOfType<SingleEffect_Controller>();
            _card_Model = GetComponent<Card_Model>();
            _card_Controller = GetComponent<Card_Controller>();
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _battleModeCard_View = GetComponent<BattleModeCard_View>();
        }

        public async void OnPointerClick(PointerEventData eventData)
        {
            if (transform.parent.gameObject.GetComponent<Board>() && _isListen && _battleCryController.IsActiveCry
                && (_battleCryController._battleCryTargets_Active != Target.Self))//���������� ���� ������� �����
            {
                if (_battleCryController.BattleCryCreator == eventData.pointerClick) return;//��������� ���������� ������� ����� �� ����
                if (_battleCryController._battleCryTargets_Active == Target.SingleFriend && _battleCryController.BattleCryCreator.GetComponent<Card>().GetSide() != GetComponent<Card>().GetSide()) return;
                ApplyNewBattleCry();
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                _isListen = false;
                _battleCryController.IsActiveCry = false;
                _battleCryController.UpdateBattleCry();

                //test
                IndicatorTarget indicatorTarget = FindObjectOfType<IndicatorTarget>();
                indicatorTarget.ChangeCursorState(false);
                ///test

            }
        }

        //��������� ������ ����
        public void ApplyNewBattleCry()
        {

            foreach (AbilityCurrentCard abilityInTarget in _battleCryController._curentAbilityInTarget)
            {
                if((_battleCryController._battleCryTargetsType_Active == _card_Model._minionType) || _battleCryController._battleCryTargetsType_Active == MinionType.None )
                {   
                    //������ ���� �����
                    if(abilityInTarget == AbilityCurrentCard.Provocation /*&& _battleCryController.SideBattleCryCreator == GetComponent<Card>()._side*/)
                    {
                        _card_Controller.ProvocationAbility(true);
                    }
                    //�������� �����
                    if (abilityInTarget == AbilityCurrentCard.ChangeAtack)
                    {
                        _card_Controller.ChangeAtackValue(_battleCryController.BattleCryChangeAtackDamage);
                    }
                    //�������� ��������
                    if (abilityInTarget == AbilityCurrentCard.ChangeHealt)
                    {
                        foreach (BattleCryType cryType in _battleCryController._currentBattleCryTypes)
                        {
                            if (cryType == BattleCryType.DealDamage) //��������� ����
                            {

                                _eventEffectController.ParseDamageEvent(this);
                                _card_Controller.ChangeHealthValue(_battleCryController.BattleCryChangeHealth, ChangeHealthType.DealDamage);
                                _singleEffectController.ApplyEffect(this);                               
                            }                                

                            if (cryType == BattleCryType.Heal)//����� ����
                            {
                                if (_card_Model.HealthCard < _card_Model.MaxHealtValue)
                                {
                                    _eventEffectController.ParseHealEvent(this);
                                }
                                _card_Controller.ChangeHealthValue(_battleCryController.BattleCryChangeHealth, ChangeHealthType.Healing);
                                if (_card_Model.HealthCard > _card_Model.MaxHealtValue)
                                    _card_Model.HealthCard = _card_Model.MaxHealtValue;

                                _singleEffectController.ApplyEffect(this);

                                _battleModeCard_View.UpdateViewCard();
                                StartCoroutine(_battleModeCard_View.EffectParticle(_battleModeCard_View.HealtEffect));
                            }
                            if (cryType == BattleCryType.RaiseParametrs) //�������� ���������
                            {
                                _card_Model.MaxHealtValue += _battleCryController.BattleCryChangeHealth;
                                _card_Controller.ChangeHealthValue(_battleCryController.BattleCryChangeHealth, ChangeHealthType.RaiseParametrs);
                                _battleModeCard_View.UpdateViewCard();
                            }
                        }
                    }
                }
            }
        }
    }
}
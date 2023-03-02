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
            //пока костыль. заменить на Zenject
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

        public void ActivateBattleCry(Transform newParent) //активация боевых кличей
        {
            if (newParent == transform.parent && !_useBattleCray)
            {
                _battleModeCardView.ChangeCardViewMode();                
                _useBattleCray = true;
                if (_card_Model._battleCryType != BattleCryType.NoСry && _card_Model._battleCryType != BattleCryType.UseAbility)
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


        private void ApplyAbilityInSelf(Transform transformParent) //применяем боевой клич на себя 
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
            Debug.Log("Существо погибло");
            gameObject.SetActive(false);
        }

        #region //Ability Сondition

        private void ChoiseAbility() //выбор активной способности
        {            
            foreach (var isActive in _card_Model._activeAbility)
            {               
                if (isActive.Value == true)
                {
                    ActivateAbility(isActive.Key);
                }
            }
        }

        private void ActivateAbility(string currentNameActiveAbility) //активация активной способности
        {            
            
            Debug.Log("активация активной способности");
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

        private void ProvocationAbility() //провокация
        {
            if (_card_Model._isProvocation == true)
            {
                _card_Model._protectionImage.gameObject.SetActive(true);
            }
        }

        private void DivineShieldAbility() //божественный щит
        {
            if (_card_Model._isDivineShield == true)
            {
                Debug.Log("Нужно активировать спрайт щита");
            }
        }

        private void PermanentEffectAbility()//постоянный эффект
        {

        }

        private void ChargeAbility()//рывок
        {

        }
        public void TakeAdditionalCard() //добавление новой карты
        {
            Debug.Log("Добавленна новая карта");
            _mulliganManager.TakeOneCard(_mana_Controller.WhoMovesNow());
        }

        #endregion

        #region //Ability Action



        public void ChangeAtackValue(int incomingValue) //изменяем значение атаки
        {            
             _card_Model._atackDamageCard += incomingValue;            
             _card_Model._maxAtackValue += incomingValue;
             _battleModeCardView.UpdateViewCard();           
        }

        public void ChangeHealtValue(int incomingValue) //изменяем значение здоровья
        {
            if (_battleCryController._battleCryType == BattleCryType.Heal) //лечимся
            {
                _card_Model._healthCard += incomingValue;
                if (_card_Model._healthCard > _card_Model._maxHealtValue)
                    _card_Model._healthCard = _card_Model._maxHealtValue;
                _battleModeCardView.UpdateViewCard();
            }
            else if (_battleCryController._battleCryType == BattleCryType.DealDamage)//принимаем урон
            {
                _card_Model._healthCard -= incomingValue;
                _battleModeCardView.UpdateViewCard();
                if (_card_Model._healthCard <= 0) DiedCreature(); //событие смерти
            }
            else if (_battleCryController._battleCryType == BattleCryType.RaiseParametrs)//изменяем параметры
            {       
                _card_Model._healthCard += incomingValue;
                    if (incomingValue > 0)
                _card_Model._maxHealtValue += incomingValue;
                _battleModeCardView.UpdateViewCard();                
            }
        }

        public void UpdateSelfParametrs(int multiplicationFactor) //увеличение своих параметров в зависимости от колличества дружеских карт на столе
        {
            ChangeAtackValue(_card_Model._abilityChangeAtackDamage* multiplicationFactor);
            ChangeHealtValue(_card_Model._abilityChangeHealth * multiplicationFactor);            
        }

        public void BerserkAbility() //ярость берсерка
        {
            ChangeAtackValue(_card_Model._abilityChangeAtackDamage);
        }        

        #endregion        
    }    
}
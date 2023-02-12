using Hearthstone;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Heartstone
{
    public class ApplyBattleCry : MonoBehaviour, IPointerClickHandler
    {
        private BattleCry_Controller _battleCryController;
        private BattleModeCard_View _battleSettings;
        private Card_Model _card_Model;
        private Card_Controller _card_Controller;

        private void OnEnable()
        {
            _card_Model = GetComponent<Card_Model>();
            _card_Controller = GetComponent<Card_Controller>();
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _battleSettings = GetComponent<BattleModeCard_View>();            
            _card_Controller = GetComponent<Card_Controller>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (transform.parent.gameObject.GetComponent<Board>())
            {
                Debug.Log("Есть изменение показателей");
                _battleSettings._atackText.text = (Int32.Parse(_battleSettings._atackText.text) + _battleCryController._currentValueChangeAtackDamage).ToString();
                _battleSettings._healthText.text = (Int32.Parse(_battleSettings._healthText.text) +_battleCryController._currentValueChangeHealth).ToString();
                _battleCryController._isActiveCry = false;
                _battleCryController._targetBattleCry.gameObject.SetActive(false);
                _battleCryController._currentValueChangeHealth = 0;
                _battleCryController._currentValueChangeAtackDamage = 0;
            }
        }
    }
}
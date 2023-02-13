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
        private PageBook_Model _pageBook_Model;

        private void OnEnable()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
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
                Debug.Log("���� ��������� �����������");
                /////////////////////////////////////////////////////////////////
                AplyNewValueCardProperty(_battleCryController._idBattleCry); ////���������� ���������� �������� �������� ID �����
                /////////////////////////////////////////////////////////////////
                _battleCryController._isActiveCry = false;
                _battleCryController._targetBattleCry.gameObject.SetActive(false);
                _battleCryController._currentValueChangeHealth = 0;
                _battleCryController._currentValueChangeAtackDamage = 0;
            }
        }


        public void AplyNewValueCardProperty(int idCard)
        {
            CardSO_Model card_SO = _pageBook_Model._cardsDictionary[idCard];
            switch (card_SO._battleCryType)
            {
                case BattleCryType.DealDamage:
                    //����� ��������� �����
                    break;
                case BattleCryType.RaiseParametrs:
                    //����� ���������� �����������
                    break;
                case BattleCryType.Heal:
                    //����� ������� ��������� ����
                    break;
                /*case BattleCryType.GetCardInDeck:
                    // ����� ���������� ����� � ����
                    break;
                case BattleCryType.SummonAssistant:
                    //����� ���������� ������� 
                    break;*/

            }
        }
    }
}
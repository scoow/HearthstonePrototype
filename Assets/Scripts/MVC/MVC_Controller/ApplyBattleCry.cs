using UnityEngine;
using UnityEngine.EventSystems;


namespace Hearthstone
{
    public class ApplyBattleCry : MonoBehaviour, IPointerClickHandler
    {
        private BattleCry_Controller _battleCryController;
        private BattleModeCard_View _battleModeCard_View;
        private Card_Model _card_Model;
        private Card_Controller _card_Controller;
        private PageBook_Model _pageBook_Model;
        public bool _isListen = true; //����� �� ������� ����� ������ ����

        private void OnEnable()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
            _card_Model = GetComponent<Card_Model>();
            _card_Controller = GetComponent<Card_Controller>();
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _battleModeCard_View = GetComponent<BattleModeCard_View>();          
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (transform.parent.gameObject.GetComponent<Board>() && _isListen && _battleCryController._isActiveCry)
            {                 
                AplyNewValueCardProperty(_battleCryController._idBattleCry);
                _isListen = false;
                _battleCryController._isActiveCry = false;                
                _battleCryController.UpdateBattleCry();
            }
        }


        public void AplyNewValueCardProperty(int idCard)
        {            
            CardSO_Model card_SO = _pageBook_Model._cardsDictionary[idCard];
            switch (card_SO._battleCryType)
            {
                // ������� ����� ������� � ��������� ������ 
                case BattleCryType.DealDamage: //�������� ����
                    _card_Model._healthCard -= card_SO._abilityChangeHealth;
                    _battleModeCard_View.UpdateViewCard();
                    if (_card_Model._healthCard <= 0) _card_Controller.DiedCreature(); //������� ������
                    
                    break;
                case BattleCryType.RaiseParametrs: //�������� ���������
                    _card_Model._healthCard += card_SO._abilityChangeHealth;
                    _card_Model._maxHealtValue = _card_Model._healthCard;                    
                    _card_Model._atackDamageCard += card_SO._abilityChangeAtackDamage;
                    _card_Model._maxAtackValue = _card_Model._atackDamageCard;
                    _battleModeCard_View.UpdateViewCard();
                    break;
                case BattleCryType.Heal://����� ��������
                    _card_Model._healthCard += card_SO._abilityChangeHealth;                    
                    if (_card_Model._healthCard > _card_Model._maxHealtValue)
                        _card_Model._maxHealtValue = _card_Model._healthCard;
                    _battleModeCard_View.UpdateViewCard();
                    break;
            }
        }
    }
}
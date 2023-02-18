using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Hearthstone
{
    public class BattleModeCard_View : MonoBehaviour
    {
        private LoadDeck_Controller _loadDeckController;
        
        private Card_Model _card_Model;
        private Card_Controller _card_Controller;
        private BattleCry_Controller _battleCryController;
        public Text _atackText;
        public Text _healthText;        
        [SerializeField] private SpriteRenderer _spriteCard;
        [SerializeField] private InFieldViewMarker _inFieldView;
        [SerializeField] private InHeadViewMarker _inHeadView;
        [SerializeField] private Transform _parent;
        /// <summary>
        /// события перетаскивания карты на стол
        /// </summary>
        private Board _board;

        private void OnEnable()
        {
            _loadDeckController = FindObjectOfType<LoadDeck_Controller>();//тест Zenject (неудачный)
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _board = FindObjectOfType<Board>();

             _card_Model = GetComponent<Card_Model>();
            _card_Controller = GetComponent<Card_Controller>();

            _loadDeckController.SetSettings += SetSettingsCardInBattle;

            _board.EndDragCard += ChangeViewCard;
        }
        private void OnDisable()
        {
            _loadDeckController.SetSettings -= SetSettingsCardInBattle;
            _board.EndDragCard -= ChangeViewCard;
        }

        public void SetSettingsCardInBattle()
        {                
            UpdateViewCard();
            _spriteCard.sprite = _card_Model._spriteCard;            
            _inFieldView.gameObject.SetActive(false);
        }

        public void UpdateViewCard()
        {
            _atackText.text = _card_Model._atackDamageCard.ToString();
            _healthText.text = _card_Model._healthCard.ToString();
        }

        public void ChangeViewCard(Transform newParent) //изменение внешнего вида карты при установке на стол , активация боевых кличей
        {
            Debug.Log("2 : " + this);
           // _parent = newParent;

            if (newParent == transform.parent)
            {
                _inFieldView.gameObject.SetActive(true);
                if(_card_Model._battleCryType != BattleCryType.NoСry)
                {
                    _battleCryController._idBattleCry = _card_Model._idCard;
                    _battleCryController._isActiveCry = true;
                    _battleCryController.UpdateBattleCry();
                }                
                _inHeadView.gameObject.SetActive(false);                                
            }                     
        }
    }
}
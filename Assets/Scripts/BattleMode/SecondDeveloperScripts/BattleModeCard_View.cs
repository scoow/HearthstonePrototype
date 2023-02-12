using Heartstone;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class BattleModeCard_View : MonoBehaviour
    {
        private LoadDeck_Controller _loadDeck_Controller;
        private Card_Model _card_Model;
        private Card_Controller _card_Controller;
        private BattleCry_Controller _battleCryController;
        public Text _atackText;
        public Text _healthText;
        public int _idCard;
        [SerializeField] private SpriteRenderer _spriteCard;
        [SerializeField] private InFieldViewMarker _inFieldView;
        [SerializeField] private InHeadViewMarker _inHeadView;
        [SerializeField] private Transform _parent;
        /// <summary>
        /// события перетаскивания карты на стол
        /// </summary>
        private CardHolder _cardHolder;
        


        private void OnEnable()
        {
            _parent = FindObjectOfType<Board>().GetComponent<Transform>();
            _loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();
            _battleCryController = FindObjectOfType<BattleCry_Controller>();
            _cardHolder = FindObjectOfType<CardHolder>();
            _card_Model = GetComponent<Card_Model>();
            _card_Controller = GetComponent<Card_Controller>();            
            
            _loadDeck_Controller.SetSettings += SetSettingsCardInBattle;
            
            _cardHolder.EndDragCard += ChangeViewCard;
        }
        private void OnDisable()
        {
            _loadDeck_Controller.SetSettings -= SetSettingsCardInBattle;
            _cardHolder.EndDragCard -= ChangeViewCard;
        }

        private void SetSettingsCardInBattle()
        {
            _idCard = _card_Model._idCard;
            _atackText.text = _card_Model._atackDamageCard.ToString();
            _healthText.text = _card_Model._healthCard.ToString();
            _spriteCard.sprite = _card_Model._spriteCard;
            
            _inFieldView.gameObject.SetActive(false);
        }
        

        public void ChangeViewCard() //изменение внешнего вида карты при установке на стол , активация боевых кличей
        {
            if (transform.parent.gameObject.GetComponent<Board>())
            {
                _inFieldView.gameObject.SetActive(true);
                _battleCryController.SaveCurrentValueBattleCry(_card_Model._idCard/*_idCard*/);
                _inHeadView.gameObject.SetActive(false);
                                
            }                            
        }
    }
}
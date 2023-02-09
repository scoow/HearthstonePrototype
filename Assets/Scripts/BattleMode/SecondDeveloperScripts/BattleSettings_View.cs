using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class BattleSettings_View : MonoBehaviour
    {
        private LoadDeck_Controller loadDeck_Controller;
        private CardSettings_Model cardSettings_Model;
        [SerializeField] private Text _atackText;
        [SerializeField] private Text _healthText;        
        [SerializeField] private SpriteRenderer _spriteCard;
        [SerializeField] private InFieldViewMarker _inFieldView;
        [SerializeField] private InHeadViewMarker _inHeadView;
        [SerializeField] private Transform _parent;
        private CardHolder _cardHolder;


        private void OnEnable()
        {
            _parent = FindObjectOfType<Board>().GetComponent<Transform>();
            cardSettings_Model = GetComponent<CardSettings_Model>();
            loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();
            _cardHolder = FindObjectOfType<CardHolder>();
            loadDeck_Controller.SetSettings += SetSettingsCardInBattle;
            _cardHolder.EndDragCard += ChangeViewCard;
        }
        private void OnDisable()
        {
            loadDeck_Controller.SetSettings -= SetSettingsCardInBattle;
            _cardHolder.EndDragCard -= ChangeViewCard;
        }

        private void SetSettingsCardInBattle()
        {
            _atackText.text = cardSettings_Model.AtackDamage.text;
            _healthText.text = cardSettings_Model.Healt.text;
            _spriteCard.sprite = cardSettings_Model.SpriteCard.sprite;
            _inFieldView.gameObject.SetActive(false);
        }

        public void ChangeViewCard()
        {
            if (transform.parent.gameObject.GetComponent<Board>())
            {
                _inFieldView.gameObject.SetActive(true);
                _inHeadView.gameObject.SetActive(false);
            }                            
        }
    }
}
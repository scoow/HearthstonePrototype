using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Hearthstone
{
    public class BattleModeCard_View : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteCard;
        [SerializeField] private InFieldViewMarker _inFieldView;
        [SerializeField] private InHeadViewMarker _inHeadView;
        private LoadDeck_Controller _loadDeckController;        
        private Card_Model _card_Model;        
        public Text _atackText;
        public Text _healthText;        
        

        private void OnEnable()
        {
            _loadDeckController = FindObjectOfType<LoadDeck_Controller>();//���� Zenject (���������)
            _card_Model = GetComponent<Card_Model>();            

            _loadDeckController.SetSettings += SetSettingsCardInBattle;            
        }
        private void OnDisable()
        {
            _loadDeckController.SetSettings -= SetSettingsCardInBattle;            
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
            if (transform.parent.gameObject.GetComponent<Board>())
            {
                if (_card_Model._atackDamageCard > _card_Model._defaultAtackValue)//�������� ���� ������ �����
                    _atackText.color = Color.green;
                if (_card_Model._atackDamageCard < _card_Model._maxAtackValue)
                    _atackText.color = Color.red;
                if (_card_Model._healthCard > _card_Model._defaultHealtValue) //�������� ���� ������ ��������
                    _healthText.color = Color.green;
                if (_card_Model._healthCard < _card_Model._maxHealtValue)
                    _healthText.color = Color.red;
            }           
        }
        
        public void ChangeCardViewMode()
        {
            _inFieldView.gameObject.SetActive(true);
            _inHeadView.gameObject.SetActive(false);
        }
    }
}
using System.Collections;
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
        
        public ParticleSystem _healtEffect; //ссылка на систему частиц HealthEffect
        public ParticleSystem _scaleEffect; //ссылка на систему частиц HealthEffect
        

        private void OnEnable()
        {            
            _loadDeckController = FindObjectOfType<LoadDeck_Controller>();//тест Zenject (неудачный)
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
                if (_card_Model._atackDamageCard > _card_Model._defaultAtackValue)//изменяем цвет текста атаки
                    _atackText.color = Color.green;
                if (_card_Model._atackDamageCard <= _card_Model._defaultAtackValue)
                    _atackText.color = Color.white;
                
                if (_card_Model._healthCard > _card_Model._defaultHealtValue) //изменяем цвет текста здоровья
                    _healthText.color = Color.green;
                if (_card_Model._healthCard < _card_Model._maxHealtValue)
                    _healthText.color = Color.red;
                if (_card_Model._healthCard == _card_Model._defaultHealtValue)
                    _healthText.color = Color.white;
            }           
        }
        
        public void ChangeCardViewMode()
        {
            _inFieldView.gameObject.SetActive(true);
            _inHeadView.gameObject.SetActive(false);
        }

        public IEnumerator EffectParticle(ParticleSystem particleSystemExample)
        {
            particleSystemExample.gameObject.SetActive(true);
            particleSystemExample.Play();
            yield return new WaitForSeconds(2f);
            particleSystemExample.gameObject.SetActive(false);
            particleSystemExample.Stop();
        }
    }
}
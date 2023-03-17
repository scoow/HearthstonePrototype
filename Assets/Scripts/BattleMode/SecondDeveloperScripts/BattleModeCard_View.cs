using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Hearthstone
{
    public class BattleModeCard_View : MonoBehaviour
    {
        private LoadDeck_Controller _loadDeckController;
        private Card_Model _card_Model;
        [SerializeField] private SpriteRenderer _spriteCard;
        [SerializeField] private InFieldViewMarker _inFieldView;
        [SerializeField] private InHeadViewMarker _inHeadView;
        [SerializeField] private ParticleSystem _healtEffect; //ссылка на систему частиц HealthEffect
        [SerializeField] private ParticleSystem _scaleEffect; //ссылка на систему частиц ScalleEffect
        [SerializeField] private Text _atackText;
        [SerializeField] private Text _healthText;        
        public Text AtackText { get => _atackText; set => _atackText = value; }        
        public Text HealthText { get => _healthText; set => _healthText = value; }        
        public ParticleSystem HealtEffect { get => _healtEffect; }
        public ParticleSystem ScaleEffect { get => _scaleEffect; }

        private void OnEnable()
        {            
            _loadDeckController = FindObjectOfType<LoadDeck_Controller>();//тест Zenject (неудачный)
            _card_Model = GetComponent<Card_Model>();
            _loadDeckController.SetCardSettings += SetSettingsCardInBattle;            
        }
        private void OnDisable()
        {
            _loadDeckController.SetCardSettings -= SetSettingsCardInBattle;            
        }

        public void SetSettingsCardInBattle()
        {                
            UpdateViewCard();
            _spriteCard.sprite = _card_Model.SpriteCard;            
            _inFieldView.gameObject.SetActive(false);
        }

        public void UpdateViewCard()
        {
            _atackText.text = _card_Model.AtackDamageCard.ToString();
            _healthText.text = _card_Model.HealthCard.ToString();
            if (transform.parent.gameObject.GetComponent<Board>())
            {
                if (_card_Model.AtackDamageCard > _card_Model.DefaultAtackValue)//изменяем цвет текста атаки
                    _atackText.color = Color.green;
                if (_card_Model.AtackDamageCard <= _card_Model.DefaultAtackValue)
                    _atackText.color = Color.white;
                
                if (_card_Model.HealthCard > _card_Model.DefaultHealtValue) //изменяем цвет текста здоровья
                    _healthText.color = Color.green;
                if (_card_Model.HealthCard == _card_Model.DefaultHealtValue)
                    _healthText.color = Color.white;
                if (_card_Model.HealthCard < _card_Model.MaxHealtValue)
                    _healthText.color = Color.red;                
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
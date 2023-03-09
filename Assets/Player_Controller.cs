using Hearthstone;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour , IChangeHealth
{
    public int _health;
    public Text textHealth;
    

    private void Start()
    {
        //textHealth = GetComponentInChildren<TextHealthMarker>().GetComponent<Text>();
    }

    public void ChangeHealthValue(int incomingValue, ChangeHealthType changeHealthType)
    {
       
        if (changeHealthType == ChangeHealthType.DealDamage)
        {
            /*_card_Model._healthCard += incomingValue;
           
            _singleEffect_Controller.ApplyEffect(this);
            if (_card_Model._healthCard <= 0)
                DiedCreature(); //событие смерти*/
        }

        if (changeHealthType == ChangeHealthType.Healing)
        {
            /*ChangeHealtValue(incomingValue);
            if (_card_Model._healthCard > _card_Model._maxHealtValue)
                _card_Model._healthCard = _card_Model._maxHealtValue;

            _singleEffect_Controller.ApplyEffect(this);
            StartCoroutine(_battleModeCardView.EffectParticle(_battleModeCardView._healtEffect));
            _battleModeCardView.UpdateViewCard();*/
        }
        
    }

}
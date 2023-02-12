using UnityEngine;

namespace Hearthstone
{
    public class Card_Controller : MonoBehaviour
    {
        private Card_Model _card_Model;       


        private void OnEnable()
        {
            _card_Model = GetComponent<Card_Model>();
        }


        private void Start()
        {
            ProvocationAbility();
        }

        #region //Ability Сondition
        private void ProvocationAbility()
        {
            if (_card_Model._isProvocation == true)
            {
                _card_Model._protectionImage.gameObject.SetActive(true);
            }
        }

        private void DivineShieldAbility()
        {
            if (_card_Model._isDivineShield == true)
            {
                Debug.Log("Нужно активировать спрайт щита");
            }
        }

        private void PermanentEffectAbility()
        {

        }

        private void ChargeAbility()
        {

        }

        private void BerserkAbility()
        {

        }
        #endregion

        #region //Ability Action

        //способность изменять здоровье
        private void ChangeHealthAbility()
        {
            if (_card_Model._abilityChangeHealth != 0)
            {
                if (_card_Model._abilityChangeHealth > 0) //если способность увеличивает здоровье
                {
                    Debug.Log("Я увеличиваю здоровье");
                }
                if (_card_Model._abilityChangeHealth < 0) //если способность уменьшает здоровье
                {
                    Debug.Log("Я уменьшаю здоровье");
                }
            }
        }

        //способность изменять атаку
        private void ChangeAtackDamageAbility()
        {
            if (_card_Model._abilityChangeAtackDamage != 0)
            {
                if (_card_Model._abilityChangeAtackDamage > 0)//если способность увеличивает атаку
                {
                    if (_card_Model._isBerserk)
                    {
                        _card_Model._atackDamageCard += _card_Model._abilityChangeAtackDamage;
                    }

                    Debug.Log("Я увеличиваю атаку");
                }

                if (_card_Model._abilityChangeAtackDamage < 0)//если способность уменьшает атаку
                {
                    Debug.Log("Я меньшаю атаку");
                }
            }
        }

        #endregion

    }

}
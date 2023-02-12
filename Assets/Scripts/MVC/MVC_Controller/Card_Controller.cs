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

        #region //Ability �ondition
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
                Debug.Log("����� ������������ ������ ����");
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

        //����������� �������� ��������
        private void ChangeHealthAbility()
        {
            if (_card_Model._abilityChangeHealth != 0)
            {
                if (_card_Model._abilityChangeHealth > 0) //���� ����������� ����������� ��������
                {
                    Debug.Log("� ���������� ��������");
                }
                if (_card_Model._abilityChangeHealth < 0) //���� ����������� ��������� ��������
                {
                    Debug.Log("� �������� ��������");
                }
            }
        }

        //����������� �������� �����
        private void ChangeAtackDamageAbility()
        {
            if (_card_Model._abilityChangeAtackDamage != 0)
            {
                if (_card_Model._abilityChangeAtackDamage > 0)//���� ����������� ����������� �����
                {
                    if (_card_Model._isBerserk)
                    {
                        _card_Model._atackDamageCard += _card_Model._abilityChangeAtackDamage;
                    }

                    Debug.Log("� ���������� �����");
                }

                if (_card_Model._abilityChangeAtackDamage < 0)//���� ����������� ��������� �����
                {
                    Debug.Log("� ������� �����");
                }
            }
        }

        #endregion

    }

}
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class BattleCry_Controller : MonoBehaviour
    {
        public bool _isActiveCry = false;
        private PageBook_Model _pageBook_Model;        
        public Transform _targetBattleCry;
        public Transform _parentCardInBattle;        
        public int _idBattleCry;
        public List<int> _activePermanentEffect;

        public BattleCryType _battleCryType;
        public BattleCryTargets _battleCryTargets;
        public MinionType _battleCryTargetsType;
        public MinionType _minionType;

        private void OnEnable()
        {            
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
            _targetBattleCry.gameObject.SetActive(false);
        }      

        private void Update() //����������� �������
        {
            if (_isActiveCry && �onditionsTargetBattleCry())
            {                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    _targetBattleCry.transform.position = raycastHit.point;
                    _targetBattleCry.transform.LookAt(Camera.main.transform);
                }
            }          
        }

        public void UpdateBattleCry() //��� ��������� ����� ����� �� ���� , � ���� ���������� ����������� ������� ������ ����
        {            
            ApplyBattleCry[] _temporaryArray = _parentCardInBattle.GetComponentsInChildren<ApplyBattleCry>();
            for (int i = 0; i <= _temporaryArray.Length - 1; i++)
            {
                if(_isActiveCry)// ���� ������ ���� �������, �� ����� ����� �������� ������ ����
                {
                    _temporaryArray[i]._isListen = true;
                    if(_battleCryTargets == BattleCryTargets.AllFriends)
                    {
                        CardSO_Model cardSO = _pageBook_Model._cardsDictionary[_idBattleCry];
                        Card_Controller card_Controller = _temporaryArray[i].GetComponent<Card_Controller>();
                        BattleModeCard_View battleModeCard = _temporaryArray[i].GetComponent<BattleModeCard_View>();
                        if (_battleCryType == BattleCryType.RaiseParametrs)
                        {
                            card_Controller.ChangeAtackValue(cardSO._abilityChangeAtackDamage);
                            card_Controller.ChangeHealtValue(cardSO._abilityChangeHealth);
                            StartCoroutine(battleModeCard.EffectParticle(battleModeCard._scaleEffect));
                        }
                        if(_battleCryType == BattleCryType.Heal)// ��������� ��������
                        {
                            card_Controller.ChangeHealtValue(cardSO._abilityChangeHealth);
                            StartCoroutine(battleModeCard.EffectParticle(battleModeCard._healtEffect));
                        }
                        
                    }
                }    
                    
                if(!_isActiveCry)//���� ������ ���� ���������, �� ����� �� ������� ������ ����
                    _temporaryArray[i]._isListen = false;

                
            }

            if(�onditionsTargetBattleCry()) //������� ��������� ������� 
            _targetBattleCry.gameObject.SetActive(_isActiveCry);            
        }


        private bool �onditionsTargetBattleCry()
        {
           bool result = _battleCryTargets != BattleCryTargets.Self 
                && _battleCryTargets != BattleCryTargets.AllFriends
                && (_battleCryType == BattleCryType.Heal
                    || _battleCryType == BattleCryType.DealDamage
                    || _battleCryType == BattleCryType.RaiseParametrs);

            return result;
        }



    }
}
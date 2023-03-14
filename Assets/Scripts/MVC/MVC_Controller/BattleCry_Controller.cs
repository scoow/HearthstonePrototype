using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class BattleCry_Controller : MonoBehaviour
    {
        public bool _isActiveCry = false;
        private PageBook_Model _pageBook_Model;        
        public IndicatorTarget _cursorBattleCry;
        public Transform _parentCardInBattle; 
        public int _idBattleCry;

        public GameObject _battleCryCreator;

        public int _battleCryChangeHealth;
        public int _battleCryChangeAtackDamage;        
        
        /// <summary>
        /// ���� ������ ������
        /// </summary>
        public List<BattleCryType> _currentBattleCryTypes = new List<BattleCryType>();
        /// <summary>
        /// ����� ������������ ������������ ������ ������
        /// </summary>
        public List<AbilityCurrentCard> _curentAbilityInTarget = new List<AbilityCurrentCard>();
        
        public Target _battleCryTargets_Active;
        public MinionType _battleCryTargetsType_Active;
        public MinionType _battleCryMinionType_Active;

        private void OnEnable()
        {            
            _pageBook_Model = FindObjectOfType<PageBook_Model>();   
        }      

        private void LateUpdate() //����������� �������
        {
            if (_isActiveCry && �onditionsTargetBattleCry())
            {          
                _cursorBattleCry.SetWatcher(_battleCryCreator.transform);
            }          
        }

        //������� ���������� ������� �����
        public bool �onditionsTargetBattleCry()
        {
            bool result = _battleCryTargets_Active == Target.Single || _battleCryTargets_Active == Target.SingleFriend;
            return result;
        }

        public void UpdateBattleCry() //��� ��������� ����� ����� �� ���� , � ���� ���������� ����������� ������� ������ ����
        {   
            ApplyBattleCry[] _temporaryArray = _parentCardInBattle.GetComponentsInChildren<ApplyBattleCry>();
            for (int i = 0; i <= _temporaryArray.Length - 1; i++)
            {
                if(_isActiveCry)// ���� ������ ���� �������, �� ����� ����� �������� ������ ����               
                    _temporaryArray[i]._isListen = true;                 
                    
                if(!_isActiveCry)//���� ������ ���� ���������, �� ����� �� ������� ������ ����
                    _temporaryArray[i]._isListen = false;               
            }

            if (�onditionsTargetBattleCry()) //������� ��������� ������� 
                _cursorBattleCry.ChangeCursorState(true);
        }       
    }
}
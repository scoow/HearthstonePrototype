using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hearthstone
{
    public class BattleCry_Controller : MonoBehaviour
    {        
        //private PageBook_Model _pageBook_Model;        
        [SerializeField] private IndicatorTarget _cursorBattleCry;
        [SerializeField] private Transform _parentCardInBattle;
        [SerializeField] private GameObject _battleCryCreator;
        [SerializeField] private bool _isActiveCry = false;
        [SerializeField] private int _idBattleCry;        
        [SerializeField] private int _battleCryChangeHealth;
        [SerializeField] private int _battleCryChangeAtackDamage;
        

        public IndicatorTarget CursorBattleCry { get => _cursorBattleCry; }
        public Transform ParentCardInBattle { get => _parentCardInBattle; }
        public GameObject BattleCryCreator { get => _battleCryCreator; set => _battleCryCreator = value; }
        public bool IsActiveCry { get => _isActiveCry; set => _isActiveCry = value; }
        public int IdBattleCry { get => _idBattleCry; set => _idBattleCry = value; }        
        public int BattleCryChangeHealth { get => _battleCryChangeHealth; set => _battleCryChangeHealth = value; }
        public int BattleCryChangeAtackDamage { get => _battleCryChangeAtackDamage; set => _battleCryChangeAtackDamage = value; }
        
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
            //_pageBook_Model = FindObjectOfType<PageBook_Model>();   
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
            bool result = _isActiveCry && _battleCryTargets_Active == Target.Single || _battleCryTargets_Active == Target.SingleFriend;//?????
            return result;
        }

        public void UpdateBattleCry() //��� ��������� ����� ����� �� ���� , � ���� ���������� ����������� ������� ������ ����
        {   
            ApplyBattleCry[] _temporaryArray = _parentCardInBattle.GetComponentsInChildren<ApplyBattleCry>();
            for (int i = 0; i <= _temporaryArray.Length - 1; i++)
            {
                if(_isActiveCry)// ���� ������ ���� �������, �� ����� ����� �������� ������ ����               
                    _temporaryArray[i].IsListen = true;                 
                    
                if(!_isActiveCry)//���� ������ ���� ���������, �� ����� �� ������� ������ ����
                    _temporaryArray[i].IsListen = false;               
            }

            if (�onditionsTargetBattleCry()) //������� ��������� ������� 
                _cursorBattleCry.ChangeCursorState(true);
        }       
    }
}
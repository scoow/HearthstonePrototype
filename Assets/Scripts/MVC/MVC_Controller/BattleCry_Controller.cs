using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Hearthstone
{
    public class BattleCry_Controller : MonoBehaviour
    {        
        [Inject]
        private IndicatorTarget _cursorBattleCry;
        [SerializeField] private Transform _parentCardInBattle;
        [SerializeField] private GameObject _battleCryCreator;
        [SerializeField] private bool _isActiveCry = false;
        [SerializeField] private int _idBattleCry;        
        [SerializeField] private int _battleCryChangeHealth;
        [SerializeField] private int _battleCryChangeAtackDamage;
        [SerializeField] private Players _sideBattleCryCreator;

        ///// <summary>
        ///// ��������� ���� �� ����� ������� ������
        ///// </summary>
        //private List<Card_Controller> _cardInBoardFirst = new();
        ///// <summary>
        ///// ��������� ���� �� ����� ������� ������
        ///// </summary>
        //private List<Card_Controller> _cardInBoardSecond = new();



        public IndicatorTarget CursorBattleCry { get => _cursorBattleCry; }
        public Transform ParentCardInBattle { get => _parentCardInBattle; }
        public GameObject BattleCryCreator { get => _battleCryCreator; set => _battleCryCreator = value; }
        public bool IsActiveCry { get => _isActiveCry; set => _isActiveCry = value; }
        public int IdBattleCry { get => _idBattleCry; set => _idBattleCry = value; }      
        public Players SideBattleCryCreator { get => _sideBattleCryCreator; set => _sideBattleCryCreator = value; }      
        public int BattleCryChangeHealth { get => _battleCryChangeHealth; set => _battleCryChangeHealth = value; }
        public int BattleCryChangeAtackDamage { get => _battleCryChangeAtackDamage; set => _battleCryChangeAtackDamage = value; }
        
        /// <summary>
        /// ���� ������ ������
        /// </summary>
        public List<BattleCryType> _currentBattleCryTypes = new();
        /// <summary>
        /// ����� ������������ ������������ ������ ������
        /// </summary>
        public List<AbilityCurrentCard> _curentAbilityInTarget = new();
        
        public Target _battleCryTargets_Active;
        public MinionType _battleCryTargetsType_Active;
        public MinionType _battleCryMinionType_Active;

/*        private void LateUpdate() //����������� �������
        {
            if (�onditionsTargetBattleCry())
            {          
                _cursorBattleCry.SetWatcher(_battleCryCreator.GetComponent<Card_Model>());
            }          
        }*/

        //������� ���������� ������� �����
        public bool �onditionsTargetBattleCry()
        {
            return _isActiveCry && _battleCryTargets_Active == Target.Single || _battleCryTargets_Active == Target.SingleFriend;//?????
        }

        public void UpdateBattleCry() //��� ��������� ����� ����� �� ���� , � ���� ���������� ����������� ������� ������ ����
        {   
            ApplyBattleCry[] _temporaryArray = _parentCardInBattle.GetComponentsInChildren<ApplyBattleCry>();
            for (int i = 0; i <= _temporaryArray.Length - 1; i++)
            {
                _temporaryArray[i].IsListen = _isActiveCry;

                /*if (_isActiveCry)// ���� ������ ���� �������, �� ����� ����� �������� ������ ����               
                    _temporaryArray[i].IsListen = true;                 
                    
                if(!_isActiveCry)//���� ������ ���� ���������, �� ����� �� ������� ������ ����
                    _temporaryArray[i].IsListen = false;   */            
            }

            if (�onditionsTargetBattleCry()) //������� ��������� ������� 
            {
                _cursorBattleCry.SetWatcher(_battleCryCreator.GetComponent<Card_Model>());
                _cursorBattleCry.ChangeCursorState(true);
            }
                
        }       
    }
}
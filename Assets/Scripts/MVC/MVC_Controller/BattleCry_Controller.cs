using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class BattleCry_Controller : MonoBehaviour
    {
        public bool _isActiveCry = false;
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
            _targetBattleCry.gameObject.SetActive(false);
        }      

        private void Update() //отображение прицела
        {
            if (_isActiveCry && (_battleCryTargets != BattleCryTargets.Self))
            {                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    _targetBattleCry.transform.position = raycastHit.point;
                    _targetBattleCry.transform.LookAt(Camera.main.transform);
                }
            }          
        }

        public void UpdateBattleCry() //при установке новой карты на стол , у всех появляется возможность принять боевой клич
        {            
            ApplyBattleCry[] _temporaryArray = _parentCardInBattle.GetComponentsInChildren<ApplyBattleCry>();
            for (int i = 0; i <= _temporaryArray.Length - 1; i++)
            {
                if(_isActiveCry)
                    _temporaryArray[i]._isListen = true;
                if(!_isActiveCry)
                    _temporaryArray[i]._isListen = false;

                Card_Model card_Model = _temporaryArray[i].GetComponent<Card_Model>();
                if (card_Model._battleCryTargets == BattleCryTargets.Self)
                    card_Model.GetComponent<Card_Controller>().UpdateSelfParametrs(_temporaryArray.Length -1); //применяем боевой клич на себя
            }

            if(_battleCryTargets != BattleCryTargets.Self) //условие появления пицела 
            _targetBattleCry.gameObject.SetActive(_isActiveCry);            
        }
        

    }
}
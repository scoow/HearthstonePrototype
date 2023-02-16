using UnityEngine;

namespace Hearthstone
{
    public class BattleCry_Controller : MonoBehaviour
    {
        public bool _isActiveCry = false;
        public Transform _targetBattleCry;
        public Transform _parentCardInBattle;        
        public int _idBattleCry;

        private void OnEnable()
        {                       
            _targetBattleCry.gameObject.SetActive(false);
        }      

        private void Update()
        {
            if (_isActiveCry)
            {                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    _targetBattleCry.transform.position = raycastHit.point;
                    _targetBattleCry.transform.LookAt(Camera.main.transform);
                }
            }          
        }

        public void UpdateBattleCry() //при установке новой карты на стол , у всех появляется возмоджность принять боевой клич
        {
            ApplyBattleCry[] _temporaryArray = _parentCardInBattle.GetComponentsInChildren<ApplyBattleCry>();
            for (int i = 0; i <= _temporaryArray.Length - 1; i++)
            {
                if(_isActiveCry)
                    _temporaryArray[i]._isListen = true;
                if(!_isActiveCry)
                    _temporaryArray[i]._isListen = false;
            }
            _targetBattleCry.gameObject.SetActive(_isActiveCry);
        }      
    }
}
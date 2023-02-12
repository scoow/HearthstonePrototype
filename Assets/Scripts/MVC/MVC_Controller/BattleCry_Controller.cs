using UnityEngine;

namespace Hearthstone
{
    public class BattleCry_Controller : MonoBehaviour
    {
        public bool _isActiveCry = false;
        public Transform _targetBattleCry;
        public int _currentValueChangeHealth;
        public int _currentValueChangeAtackDamage;
        private PageBook_Model _pageBook_Model;
        public int iD;

        private void OnEnable()
        {
            _pageBook_Model =FindObjectOfType<PageBook_Model>();            
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


        public void BattleCryUpdate()//раскомментировал - баг пропал
        {
            _isActiveCry = !_isActiveCry;
            _targetBattleCry.gameObject.SetActive(_isActiveCry);
        }

        public void SaveCurrentValueBattleCry(int idCard)
        {
            iD = idCard;
            _currentValueChangeHealth = 0;
            _currentValueChangeAtackDamage = 0;
            CardSO_Model cardSO = _pageBook_Model._cardsDictionary[idCard];
            _currentValueChangeHealth = cardSO._abilityChangeHealth;
            _currentValueChangeAtackDamage = cardSO._abilityChangeAtackDamage;
            if(_currentValueChangeAtackDamage != 0 || _currentValueChangeHealth != 0)
            {
                _targetBattleCry.gameObject.SetActive(true);
                _isActiveCry = true;
            }            
        }
    }
}
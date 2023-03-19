using UnityEngine;

namespace Hearthstone
{
    public class IndicatorTarget : MonoBehaviour
    {
        //private Transform _watcherTransform;
        private Card_Model _watcherTransform;
        [SerializeField]
        private GameObject _cursor;
        [SerializeField]
        private GameObject _dottedLine;
        private bool _cursorEnabled;
        public bool CursorEnabled { get { return _cursorEnabled; } set { _cursorEnabled = value; } }//сделать покрасивше

        public void CursorBattleCryOn()
        {            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                _cursor.transform.position = raycastHit.point;
                float x = raycastHit.point.x - _watcherTransform.transform.position.x;
                float y = raycastHit.point.y - _watcherTransform.transform.position.y;
                float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                _dottedLine.transform.localScale = new Vector3((Mathf.Sqrt((x * x) + (y * y)) / 10) - 0.2f, _dottedLine.transform.localScale.y, _dottedLine.transform.localScale.z);
                _dottedLine.transform.position = new Vector3(_watcherTransform.transform.position.x, _watcherTransform.transform.position.y, _dottedLine.transform.position.z);
                _dottedLine.transform.rotation = Quaternion.Euler(0, 0, angle);
                _cursor.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        public GameObject GetWatcher()
        {
            return _watcherTransform.gameObject;
        }
        public void SetWatcher(Card_Model creatorBattleCry)
        {
            ChangeCursorState(true);
            _watcherTransform = creatorBattleCry;
            Debug.Log("Watcher = " + creatorBattleCry.NameCard);
        }
        public void ChangeCursorState(bool newstate)
        {
            _cursor.SetActive(newstate);
            _dottedLine.SetActive(newstate);
            CursorEnabled = newstate;
        }

        private void Update()
        {
            if (CursorEnabled)
            {
                CursorBattleCryOn();
            }
        }
    }
}
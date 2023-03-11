using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.Asteroids;

public class IndicatorTarget : MonoBehaviour
{
    public Transform _targetTransform;
    public Transform _watcherTransform;
    public GameObject _cursor;
    public GameObject _dottedLine;   

    public void CursorBattleCryOn(Transform creatorBattleCry)
    {        
        _watcherTransform = creatorBattleCry;
        _cursor.SetActive(true);
        _dottedLine.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            _cursor.transform.position = raycastHit.point;            
            float x = raycastHit.point.x - _watcherTransform.position.x;
            float y = raycastHit.point.y - _watcherTransform.position.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            _dottedLine.transform.localScale = new Vector3((Mathf.Sqrt((x * x) + (y * y)) / 10) - 0.2f, _dottedLine.transform.localScale.y, _dottedLine.transform.localScale.z);
            _dottedLine.transform.position = new Vector3(_watcherTransform.position.x, _watcherTransform.position.y, _dottedLine.transform.position.z);
            _dottedLine.transform.rotation = Quaternion.Euler(0, 0, angle);
            _cursor.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
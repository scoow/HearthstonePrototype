using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInHand : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Camera _camera;
    //private Vector3 _offset;
    private void Awake()
    {
        _camera= Camera.main;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //_offset = transform.position- _camera.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = eventData.position;
        newPosition.z = _camera.transform.position.z;

        newPosition = -_camera.ScreenToWorldPoint(newPosition);

        newPosition.z = -1;
        transform.position = newPosition;
        
        //разобраться почему надо умножать на -1
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End drag");
    }
}

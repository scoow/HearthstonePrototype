using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInHand : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Camera _camera;
    private void Awake()
    {
        _camera= Camera.main;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = eventData.position;
        newPosition.z = _camera.transform.position.z;
       
        newPosition = _camera.ScreenToWorldPoint(newPosition);
        newPosition.z = -1;
        transform.position = -newPosition;
        //разобраться почему надо умножать на -1
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
    }
}

using Hearthstone;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minion : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    //расмотреть возможность сделать общего предка с CardInHand
    private Camera _camera;

    private Players _side;
    private bool _cancelDrag;

    private Mana_Controller _mana_Controller;

    private void Awake()
    {
        _camera = Camera.main;
        _side = GetComponent<BattleModeCard>().GetSide();
        _mana_Controller = FindObjectOfType<Mana_Controller>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _cancelDrag = _side != _mana_Controller.WhoMovesNow();
        if (_cancelDrag)//≈сли не наш ход - нельз€ схватить карту
        {
            return;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_cancelDrag)//≈сли не наш ход - нельз€ схватить карту
        {
            return;
        }
        Vector3 newPosition = eventData.position;
        newPosition.z = _camera.transform.position.z;

        newPosition = -_camera.ScreenToWorldPoint(newPosition);

        newPosition.z = -1;
        transform.position = newPosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_cancelDrag)//≈сли не наш ход - нельз€ схватить карту
        {
            return;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent<Minion>(out var attacker)) return;
        if (attacker.GetSide() == _side) return;
        Debug.Log("јтака");
    }
    private Players GetSide()
    {
        return _side;
    }

}

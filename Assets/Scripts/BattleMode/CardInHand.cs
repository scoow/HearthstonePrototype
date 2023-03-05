using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Hearthstone
{
    public class CardInHand : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Camera _camera;
        private LayerRenderUp _layersRenderUp;
        private GameObject _tempCardGO;

        public Transform parent;
        [Inject]
        private Mana_Controller _mana_Controller;

        private Players _side;
        private bool _cancelDrag;
        public Action<bool> BeginDrag;

        private void Awake()
        {
            _camera = Camera.main;
            _tempCardGO = GameObject.Find("TempCard");
            _layersRenderUp = GetComponent<LayerRenderUp>();
            _side = GetComponent<BattleModeCard>().GetSide();
            _mana_Controller = FindObjectOfType<Mana_Controller>();//Zenject �� ��������. ������?
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _cancelDrag = _side != _mana_Controller.WhoMovesNow() || _mana_Controller.GetManaCount(_side) < GetComponent<Card_Model>().GetManaCostCard();
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                return;
            }

            _tempCardGO.transform.position = transform.position;//��������� �����
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            _layersRenderUp.LayerUp(50);

            BeginDrag?.Invoke(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                return;
            }
            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;

            newPosition = -_camera.ScreenToWorldPoint(newPosition);

            newPosition.z = -1;
            transform.position = newPosition;            
            //����������� ������ ���� �������� �� -1
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                return;
            }
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            CardHolder _parent = transform.parent.GetComponent<CardHolder>();
            _layersRenderUp.LayerUp(-50);
            if (_parent is Hand)
            {
                transform.position = _tempCardGO.transform.position;
            }            
            _tempCardGO.transform.position = new Vector3(100, 0);//������� ��������� ����� �� ������� ������
            BeginDrag?.Invoke(false);
        }
        /// <summary>
        /// ���������� ��������
        /// </summary>
        /// <param name="cardHolder">��������</param>
        public void SetParent(CardHolder cardHolder)
        {
            parent = transform.parent = cardHolder.transform;
        }
    }
}
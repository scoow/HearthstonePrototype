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
            _mana_Controller = FindObjectOfType<Mana_Controller>();//Zenject не сработал. ѕочему?
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _cancelDrag = _side != _mana_Controller.WhoMovesNow() || _mana_Controller.GetManaCount(_side) < GetComponent<Card_Model>().GetManaCostCard();
            if (_cancelDrag)//≈сли не наш ход - нельз€ схватить карту
            {
                return;
            }

            _tempCardGO.transform.position = transform.position;//временна€ карта
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            _layersRenderUp.LayerUp(50);

            BeginDrag?.Invoke(true);
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
            //разобратьс€ почему надо умножать на -1
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_cancelDrag)//≈сли не наш ход - нельз€ схватить карту
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
            _tempCardGO.transform.position = new Vector3(100, 0);//”бираем временную карту за пределы экрана
            BeginDrag?.Invoke(false);
        }
        /// <summary>
        /// ”становить родител€
        /// </summary>
        /// <param name="cardHolder">родитель</param>
        public void SetParent(CardHolder cardHolder)
        {
            parent = transform.parent = cardHolder.transform;
        }
    }
}
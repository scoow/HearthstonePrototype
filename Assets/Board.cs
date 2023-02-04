using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class Board : CardHolder, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)//������� � �����-��������
        {
            BattleModeCard card = eventData.pointerDrag.GetComponent<BattleModeCard>();

            if (card != null)
            {
                Debug.Log("drop card");
                card.transform.position = transform.position;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class Board : CardHolder
    {
        public override void AddCard(CardInHand card)
        {
            base.AddCard(card);
            EndDragCard?.Invoke(); ///событие для смены отображения карты
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class Hand : CardHolder
    {
        public override void AddCard(CardInHand card)
        {
            if (_cardCount > 10) return;

            base.AddCard(card);
        }
    }
}
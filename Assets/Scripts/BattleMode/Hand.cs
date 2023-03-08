using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class Hand : CardHolder
    {
        public override void AddCard(Card card)
        {
            if (_cardsList.Count > 10) return;

            base.AddCard(card);
        }
    }
}
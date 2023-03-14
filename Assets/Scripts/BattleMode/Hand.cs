using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class Hand : CardHolder
    {
        private int _minimumLayer = 50;
        public override void AddCard(Card card)
        {
            if (_cardsList.Count > 10) return;

            base.AddCard(card);
            ResetCardsLayers();
        }
        private void ResetCardsLayers()
        {
            int _layer = _minimumLayer;
            foreach (Card card in _cardsList)
            {
                card._layersRenderUp.SetLayer(_layer);
                _layer++;
            }
        }
    }
}
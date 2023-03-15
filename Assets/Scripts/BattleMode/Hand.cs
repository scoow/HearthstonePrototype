using UnityEngine;

namespace Hearthstone
{
    public class Hand : CardHolder
    {
        [SerializeField]
        private int _minimumLayer = 50;//Уровень отрисовки, с которого начинается отсчёт у карт в руке
        public override void AddCard(Card card)
        {
            if (CardsCount() > 10) return;

            base.AddCard(card);
            ResetCardsLayers();
        }
        /// <summary>
        /// Выставляет layer всем картам по порядку, слева направо
        /// </summary>
        private void ResetCardsLayers()
        {
            int _layer = _minimumLayer;
            LayerRenderUp layerRenderUp;
            foreach (Card card in _cardsList)
            {
                layerRenderUp = card.GetComponent<LayerRenderUp>();
                layerRenderUp.SetLayer(_layer);
                _layer += 2;
            }
        }
    }
}
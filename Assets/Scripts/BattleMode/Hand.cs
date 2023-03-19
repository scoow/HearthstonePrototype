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

            _offset *= 0.95f;
            base.AddCard(card);
            
            ReorderCards();
            ResetCardsLayers();
        }
        public override void RemoveCard(Card card)
        {
            _offset /= 0.95f;
            base.RemoveCard(card);

            
            ReorderCards();
        }
        private void ReorderCards()
        {
            Vector3 startPosition = transform.position;
            
            foreach (Card card in _cardsList)
            {
                card.transform.position = startPosition;
                startPosition += new Vector3(_offset, 0, 0);
            }
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
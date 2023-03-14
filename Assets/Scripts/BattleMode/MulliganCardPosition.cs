using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class MulliganCardPosition : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private MulliganCardPositionEnum _position;
        private bool _selected = false;
        public bool Selected => _selected;

        private SpriteRenderer _spriteRenderer;
        private Card _mulliganCard;

        private void Awake()
        {
            _spriteRenderer= GetComponent<SpriteRenderer>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _selected = !_selected;
            SwitchRenderer(_selected);
        }
        public void SwitchRenderer(bool selected)
        {
            _spriteRenderer.enabled = selected;
            _selected = selected;
           //_mulliganCard.Selected = selected;
        }
        public void SetCurrentCard(Card card)
        {
            _mulliganCard = card;
        }
        public Card GetCurrentCard()
        {
            return _mulliganCard;
        }
    }
}
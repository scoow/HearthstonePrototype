using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class MulliganCardPosition : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private MulliganCardPositionEnum _position;
        [SerializeField]
        private bool _selected = false;

        private SpriteRenderer _spriteRenderer;
        private MulliganCard _mulliganCard;

        private void Awake()
        {
            _spriteRenderer= GetComponent<SpriteRenderer>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _selected = !_selected;
            SwitchRenderer(_selected);
        }
        private void SwitchRenderer(bool selected)
        {
            _spriteRenderer.enabled = selected;
            _mulliganCard.Selected = selected;
        }
        public void SetCurrentCard(MulliganCard card)
        {
            _mulliganCard = card;
        }
    }
}
using UnityEngine;

namespace Hearthstone
{
    public class CardSettings : MonoBehaviour
    {
        public CardSO _cardSO_Prefab;
        private Sprite _spriteRender;
        void Start()
        {
            //_spriteRender = GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>().sprite;
            //_spriteRender = _cardSO_Prefab._spriteCard;
        }
    }
}
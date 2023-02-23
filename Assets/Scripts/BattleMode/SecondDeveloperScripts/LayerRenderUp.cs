using UnityEngine;

namespace Hearthstone
{
    public class LayerRenderUp : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteCard;
        [SerializeField]
        private SpriteRenderer _spriteFront;
        [SerializeField]
        private Canvas _canvas;
        

        public void LayerUp(int counter)
        {
            _spriteCard.sortingOrder += counter;
            _spriteFront.sortingOrder += counter;
            _canvas.sortingOrder += counter; 
            
        }
        /*public void LayerLastSpriteUp()
        {
            _spriteLastSprite.sortingOrder++;
        }*/
    }
}
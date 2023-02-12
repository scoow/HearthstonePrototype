using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class TextDeckNameMarker : MonoBehaviour
    {
        public Text _deckName;
        public Image _emisionImage;
        private IActive _active;

        private DeckCollection_Marker _deckCollection_Marker;

        private void Awake()
        {
            _deckCollection_Marker = FindObjectOfType<DeckCollection_Marker>();
            _active = FindObjectOfType<Memory_Controller>();
        }

        public void ChangeState()
        {
            Transform[] _temporaryArray = _deckCollection_Marker.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in _temporaryArray)
            {
                if (child.TryGetComponent(out EmissionMarker image))
                {
                    image.gameObject.GetComponent<Image>().color = new Color(172, 172, 172, 255);
                }
            }
            _active.ChangeStateDeck(_deckName.text);
            _emisionImage.color = new Color(0, 255, 88, 255);
        }

    }
}